using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Program_DES
{
    class DES_CRYPT
    {
        string content = string.Empty;
        int chipher_mode;
        string passphrase = string.Empty;

        public DES_CRYPT(string cont,int mode, string pass)
        {
            content = cont;
            chipher_mode = mode;
            passphrase = pass;
        }

        public void Des_Encode(string outfile)
        {
            DES des = new DES();

            //хэширование парольной фразы, получение ключа 
            byte[] key = des.GetMainKey(passphrase);
            
            bool is_file = true;
            if (content.Length >= 4)
            {    
                if (content[0] == 'T' && content[1] == 'E' && content[2] == 'X' && content[3] == 'T')
                    is_file = false;        
            }
            byte[] des_content;
            if (is_file)
            {
                //запись сигнатуры в файл
                des.AddSignature(content);

                //чтение содержимого файла
                des_content = des.InputContent(content);
            }
            else
            {
                content = content.Remove(0,4);
                des_content = Encoding.UTF8.GetBytes(content + "\nIrinaZaitseva");
            }

            int t = 51; // размер блока для CFB и OFB диапазон - (1, 64)

            switch (chipher_mode)
            {
                case 1:
                    des.encoding_ECB(outfile, des_content, key);
                    break;
                case 2:
                    des.encoding_CBC(outfile, des_content, key);
                    break;
                case 3:
                    des.encoding_CFB(outfile, des_content, key, t);
                    break;
                case 4:
                    des.encoding_OFB(outfile, des_content, key, t);
                    break;
                default:
                    Console.WriteLine("Неверный режим кодировки.");
                    break;
            }
            if(is_file)
                des.DelSignature(content);
        }

        public bool Des_Decode(string inpfile, string outfile)
        {
            DES des = new DES();

            //хэширование парольной фразы, получение ключа 
            byte[] key = des.GetMainKey(passphrase);

            int t = 51; // размер блока для CFB и OFB диапазон - (1, 64)

            bool is_file = true;
            if (content.Length >= 4)
            {
                if (content[0] == 'T' && content[1] == 'E' && content[2] == 'X' && content[3] == 'T')
                    is_file = false;
            }
            byte[] des_content;
            if (is_file)
            {
                //чтение содержимого файла
                des_content = des.InputEncodeContent(inpfile);

            }
            else
            {
                content = content.Remove(0, 4);
                des_content = Encoding.Unicode.GetBytes(content);
            }

            switch (chipher_mode)
            {
                case 1:
                    des.decoding_ECB(key, des_content, outfile);
                    break;
                case 2:
                    des.decoding_CBC(key, des_content, outfile);
                    break;
                case 3:
                    des.decoding_CFB(key, des_content, outfile, t);
                    break;
                case 4:
                    des.decoding_OFB(key, des_content, outfile, t);
                    break;
            }
            if (des.CheckSign(outfile))
            {
                des.DelSignature(outfile);
                return true;
            }
            else 
                return false;           
        }
    }

    public class DES
    {
        //шифрование блоков
        private const int sizeOfBlock = 64;                                     //размер блока в битах
        private const int countOfSym = 8;                                       //количество символов в блоке (UTF-8 char = 8 бит)
        private const int countOfRounds = 16;                                   //количество раундов
        private readonly int[] BeginTransposeMatrix =
        {   58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };                      //матрица начальной перестановки 
        private readonly int[] EndTransposeMatrix = {
                                40, 08, 48, 16, 56, 24, 64, 32,
                                39, 07, 47, 15, 55, 23, 63, 31,
                                38, 06, 46, 14, 54, 22, 62, 30,
                                37, 05, 45, 13, 53, 21, 61, 29,
                                36, 04, 44, 12, 52, 20, 60, 28,
                                35, 03, 43, 11, 51, 19, 59, 27,
                                34, 02, 42, 10, 50, 18, 58, 26,
                                33, 01, 41, 09, 49, 17, 57, 25};                        //матрица конечной перестановки

        //генерация ключей
        private const int maxsizeKey = 64;                                      // максимальный размер ключа 
        private const int sizeKey = 56;                                         // размер ключа без контрольных битов
        private const int minsizeKey = 48;                                      // размер ключа на каждой итерации шифрования
        private const int shiftKey1 = 1;                                        // цикл. сдвиг ключа 1,2,9,16 итерации
        private const int shiftKey2 = 2;                                        // цикл. сдвиг ключа для остальных итераций

        private readonly int[] GMatrix =  {
        57, 49, 41, 33, 25, 17, 9,
        1, 58, 50, 42, 34, 26, 18,
        10, 2, 59, 51, 43, 35, 27,
        19, 11, 3, 60, 52, 44, 36,
        63, 55, 47, 39, 31, 23, 15,
        7, 62, 54, 46, 38, 30, 22,
        14, 6, 61, 53, 45, 37, 29,
        21, 13, 05, 28, 20, 12, 4};                                    // матрица начальной подготовки ключа
        private readonly int[] HMatrix =
        {
            14, 17, 11, 24, 1,  5,
            3,  28, 15, 6,  21, 10,
            23, 19, 12, 4,  26, 8,
            16,  7, 27, 20, 13, 2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };                                    // матрица завершающей обработки ключа

        //функция шифрования f
        private readonly int[] EMatrix =
        {
            32, 1,  2,  3,  4,  5,
            4,  5,  6,  7,  8,  9,
            8,  9,  10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32, 1
        };                                 // функция расширения для F (расширение блока R 32 -> 48 бит)
        private readonly int[,] S1_Matrix = {   {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                                {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                                {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                                {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 }
        };
        private readonly int[,] S2_Matrix = {   {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                                                {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                                                {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                                                {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
        };
        private readonly int[,] S3_Matrix = {   {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                                                {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                                                {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                                                {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}
        };
        private readonly int[,] S4_Matrix = {   {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                                                {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                                                {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                                                {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}
        };
        private readonly int[,] S5_Matrix = {   {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                                                {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                                                {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                                                {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}
        };
        private readonly int[,] S6_Matrix = {   {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                                                {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                                                {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                                                {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}
        };
        private readonly int[,] S7_Matrix = {   {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                                                {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                                                {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                                                {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12}

        };
        private readonly int[,] S8_Matrix = {   {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                                                {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                                                {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                                                {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
        };
        private readonly int[] PMatrix =
        {
            16, 07, 20, 21,
            29, 12, 28, 17,
            01, 15, 23, 26,
            05, 18, 31, 10,
            02, 08, 24, 14,
            32, 27, 03, 09,
            19, 13, 30, 06,
            22, 11, 04, 25
        };

        // для PrintInput2
        private byte ostatok_byte = 0;

        //сигнатура
        static string sign = "IrinaZaitseva";
        static byte[] signbyte = Encoding.UTF8.GetBytes(sign);
        int signlength = signbyte.Length;

        public byte[] InputContent(string inpfile)
        {    
            return File.ReadAllBytes(inpfile);
        }

        public byte[] InputEncodeContent(string inpfile)
        {
            return File.ReadAllBytes(inpfile);
        }

        public byte[] InputBlock(byte[] content, int pos) // на выходе 64 бита блока № pos
        {
            //возвращает значения 64 битов блока № pos из введенного контента
            //в байтовом представлении. Если блок короче 64 бит, дополнит его нулями.
            byte[] block = new byte[sizeOfBlock];
            byte sym = 0;
            for (int i = 0; i < countOfSym; i++)
            {
                if ((pos * countOfSym + i) < content.Length)
                    sym = (content[pos * countOfSym + i]);   // очередной символ из блока pos
                else sym = 0;

                // переписываем биты символа в массив ("а" = 97 = 0110 0001)
                for (int j = 0; j < 8; j++) //8 т.к. 1 байт = 8 бит
                {
                    block[i * 8 + j] = (byte)((sym >> (7 - j)) % 2);
                }
            }
            return block;
        }

        public byte[] InputEncodeBlock(byte[] content, int pos)
        {
            byte[] block = new byte[sizeOfBlock];
            for (int i = 0; i < sizeOfBlock; i++)
                block[i] = content[pos * sizeOfBlock + i];
            return block;
        }
        public void PrintToFile(byte[] block, string outfile, bool toChar)
        {
            //печать блока из 64 битов по байтам
            var mode = FileMode.Create;
            if (File.Exists(outfile))
                mode = FileMode.Append;       
            using (var stream = File.Open(outfile, mode))
            {
                BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false);
                if (toChar)
                {
                    ulong block2 = 0;
                    for (int j = 7; j >= 0; j--)
                        for (int i = 0; i < 8; i++)
                            block2 = (block2 << 1) + block[(8 * j) + i];
                    writer.Write(block2);
                    return;
                }
                writer.Write(block);
            }       
        }

        public void AddSignature(string outfile)
        {
            //печать блока из 64 битов по байтам          
            if (File.Exists(outfile))
                using (var stream = File.Open(outfile, FileMode.Append))
                {
                    BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false);
                    writer.Write(signbyte);
                }  
        }
        public void DelSignature(string inpfile)
        {
            if (File.Exists(inpfile))
            {
                byte[] readbyte = File.ReadAllBytes(inpfile);
                int count_nulbyte = 0;
                for (int i = readbyte.Length-1; i >= 0; i--)
                {
                    if (readbyte[i] == 0)
                        count_nulbyte++;
                    else
                        break;
                }
                using (var stream = File.Create(inpfile))
                {
                    BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false);
                    writer.Write(readbyte, 0, readbyte.Length - 13-count_nulbyte);
                }

            }              
        }

        public bool CheckSign(string outfile)
        {
            int pos = 0;
            byte[] checkDecrypt = File.ReadAllBytes(outfile);
            
            for (pos = checkDecrypt.Length - 1 ; pos >= 0 ; pos--)
                if (checkDecrypt[pos] != 0)
                    break;

            for (int i = 0; i < signlength && i <= pos; i++)
                if (checkDecrypt[pos - i] != signbyte[signlength - 1 - i])
                    return false;  
            return true;
        }

        public void BeginTranspos(ref byte[] block)
        {
            // начальная перестановка
            byte[] res_block = new byte[sizeOfBlock];
            for (int i = 0; i < sizeOfBlock; i++)
                res_block[i] = block[BeginTransposeMatrix[i] - 1];
            block = res_block;
        }

        public void EndTranspos(ref byte[] block)
        {
            // конечная перестановка
            byte[] res_block = new byte[sizeOfBlock];
            for (int i = 0; i < sizeOfBlock; i++)
                res_block[i] = block[EndTransposeMatrix[i] - 1];
            block = res_block;
        }
        public void MainEncoding(ref byte[] block, byte[] key)
        {
            //L = L(0)
            byte[] L = new byte[sizeOfBlock / 2];
            for (int i = 0; i < sizeOfBlock / 2; i++)
                L[i] = block[i];

            //R = R(0)
            byte[] R = new byte[sizeOfBlock / 2];
            for (int i = 0; i < sizeOfBlock / 2; i++)
                R[i] = block[i + sizeOfBlock / 2];

            //----------------------ВЫЧИСЛЕНИЕ КЛЮЧА K1 от G до H------------------------

            //исходный ключ key + G
            byte[] gkey = GetGKey(key);

            //С = C(0)
            byte[] C = new byte[sizeKey / 2];
            for (int i = 0; i < sizeKey / 2; i++)
                C[i] = gkey[i];
            //D = D(0)
            byte[] D = new byte[sizeKey / 2];
            for (int i = 0; i < sizeKey / 2; i++)
                D[i] = gkey[i + sizeKey / 2];

            ShiftLeftCD(ref C, 1); //C = C(1)
            ShiftLeftCD(ref D, 1); //D = D(1)

            //K = K(1) 
            byte[] K = GetHKey(MergeCD(C, D));
            //----------------------ВЫЧИСЛЕНИЕ КЛЮЧА K1 --------------------------------


            byte[] TMP_L = L;                       // L = L(0)
            L = R;                                  // L = L(1) = R0
            R = XOR(FunctionF(R, K), TMP_L);        // R = R(1) = L0 xor F (R0, K1)

            //---------------------Конец 1 итерации ----------------------------------------------------------


            for (int i = 2; i <= countOfRounds; i++)
            {
                //r1 и l1 есть
                // -----------вычисляем ключ K ------------------- 
                ShiftLeftCD(ref C, i); //C = C(2)
                ShiftLeftCD(ref D, i); //D = D(2)

                K = GetHKey(MergeCD(C, D)); //k2
                //-------------------------------------------------

                TMP_L = L;                              // L = L(1)
                L = R;                                  // L = L(2) = R1
                R = XOR(FunctionF(R, K), TMP_L);        // R = R(2) = L1 xor F (R1, K2)
            }
            for (int i = 0; i < sizeOfBlock / 2; i++)
                block[i] = R[i];
            for (int i = 0; i < sizeOfBlock / 2; i++)
                block[i + sizeOfBlock / 2] = L[i];
        }

        public void MainDecoding(ref byte[] block, byte[] key)
        {
            //----------------------ВЫЧИСЛЕНИЕ КЛЮЧА K16 от G до H------------------------
            //исходный ключ key + G
            byte[] gkey = GetGKey(key);

            byte[] C = new byte[sizeKey / 2];
            byte[] D = new byte[sizeKey / 2];
            byte[] K;

            //R = R(16)
            byte[] R = new byte[sizeOfBlock / 2];
            for (int i = 0; i < sizeOfBlock / 2; i++)
                R[i] = block[i];

            //L = L(16)
            byte[] L = new byte[sizeOfBlock / 2];
            for (int i = 0; i < sizeOfBlock / 2; i++)
                L[i] = block[i + sizeOfBlock / 2];

            byte[] TMP_R;

            for (int i = 16; i >= 1; i--)
            {
                //r16 и l16 есть

                // -----------вычисляем ключ K -------------------
                //С = C(0)
                //D = D(0)
                for (int k = 0; k < sizeKey / 2; k++)
                    C[k] = gkey[k];
                for (int k = 0; k < sizeKey / 2; k++)
                    D[k] = gkey[k + sizeKey / 2];

                for (int k = 1; k <= i; k++)
                {
                    ShiftLeftCD(ref C, k);      // C = C(i)
                    ShiftLeftCD(ref D, k);      // D = D(i)
                }
                //K = K(i) 
                K = GetHKey(MergeCD(C, D));
                //-------------------------------------------------

                TMP_R = R;                              // TMP_R = R(16)
                R = L;                                  // R = R(15) = L16
                L = XOR(FunctionF(L, K), TMP_R);        // L = L(15) = R16 xor F (L16, K16)
            }
            for (int i = 0; i < sizeOfBlock / 2; i++)
                block[i] = L[i];
            for (int i = 0; i < sizeOfBlock / 2; i++)
                block[i + sizeOfBlock / 2] = R[i];
        }

        public byte[] GetMainKey(string passphrase)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            byte[] key = new byte[maxsizeKey];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    key[i * 8 + j] = (byte)((hash[i] >> (7 - j)) % 2);
            return key;
        }

        public byte[] GetGKey(byte[] mainkey)
        {
            byte[] gkey = new byte[sizeKey];
            for (int i = 0; i < sizeKey; i++)
                gkey[i] = mainkey[GMatrix[i] - 1];
            return gkey;
        }

        public void ShiftLeftCD(ref byte[] C, int numiter)
        {
            byte c1 = C[0];
            byte c2 = C[1];
            int shiftKey = (numiter == 1 || numiter == 2 || numiter == 9 || numiter == 16) ? shiftKey1 : shiftKey2;
            for (int i = 0; i < sizeKey / 2 - shiftKey; i++)
                C[i] = C[i + shiftKey];

            if (shiftKey == 1)
                C[sizeKey / 2 - 1] = c1;
            else
            {
                C[sizeKey / 2 - 2] = c1;
                C[sizeKey / 2 - 1] = c2;
            }
        }

        public byte[] MergeCD(byte[] C, byte[] D)
        {
            byte[] merge = new byte[sizeKey];
            for (int i = 0; i < sizeKey / 2; i++)
                merge[i] = C[i];
            for (int i = sizeKey / 2; i < sizeKey; i++)
                merge[i] = D[i - sizeKey / 2];
            return merge;
        }

        public byte[] GetHKey(byte[] CD)
        {
            byte[] hkey = new byte[minsizeKey];
            for (int i = 0; i < minsizeKey; i++)
                hkey[i] = CD[HMatrix[i] - 1];
            return hkey;
        }

        public byte[] GetEFunc(byte[] R)
        {
            //функция расширения
            byte[] E = new byte[minsizeKey];
            for (int i = 0; i < minsizeKey; i++)
                E[i] = R[EMatrix[i] - 1];
            return E;
        }

        public byte[] GetPFunc(byte[] S)
        {
            byte[] P = new byte[32];
            for (int i = 0; i < 32; i++)
                P[i] = S[PMatrix[i] - 1];
            return P;
        }

        public byte[] XOR(byte[] b1, byte[] b2)
        {
            byte[] res = new byte[b1.Length];
            for (int i = 0; i < b1.Length; i++)
                res[i] = (byte)((b1[i]) ^ (b2[i]));
            return res;
        }

        public byte[] SMatrixFunc(byte[] BI, int I)
        {
            int[,] S = { };
            switch (I)
            {
                case 1:
                    S = S1_Matrix;
                    break;
                case 2:
                    S = S2_Matrix;
                    break;
                case 3:
                    S = S3_Matrix;
                    break;
                case 4:
                    S = S4_Matrix;
                    break;
                case 5:
                    S = S5_Matrix;
                    break;
                case 6:
                    S = S6_Matrix;
                    break;
                case 7:
                    S = S7_Matrix;
                    break;
                case 8:
                    S = S8_Matrix;
                    break;
            }

            int row = BI[0] * 2 + BI[5];
            int col = BI[1] * 8 + BI[2] * 4 + BI[3] * 2 + BI[4];
            int t = S[row, col];

            byte[] res = new byte[4];
            res[0] = (byte)(t / 8 % 2);
            res[1] = (byte)(t / 4 % 2);
            res[2] = (byte)(t / 2 % 2);
            res[3] = (byte)(t % 2);
            return res;
        }

        public byte[] FunctionF(byte[] R0, byte[] K1)
        {

            byte[] B = XOR(GetEFunc(R0), K1);

            //разбиваем на 8 6-битовых блоков
            byte[] BI = new byte[6];
            byte[] SI = new byte[4];            // результат Si(Bi) 
            byte[] S = new byte[32];            //итоговая S-последовательность
            for (int i = 0; i < 8; i++)
            {
                //формируем блок 6 бит
                for (int j = 0; j < 6; j++)
                    BI[j] = B[6 * i + j];

                SI = SMatrixFunc(BI, i + 1);
                //формируем S - последовательность 32 бита
                for (int k = 0; k < 4; k++)
                    S[i * 4 + k] = SI[k];
            }

            byte[] res = new byte[sizeOfBlock / 2];
            //последняя перестановка в функции f
            res = GetPFunc(S);
            return res;
        }

        
        // ----ECB:
        public void encoding_ECB(string file, byte[] content, byte[] key) 
        {
            int countOfBlocks = (content.Length % 8 != 0) ? content.Length / 8 + 1 : content.Length / 8;
            byte[] block = { };

            for (int i = 0; i < countOfBlocks; i++)
            {
                //чтение блока 64 бита
                block = InputBlock(content, i);

 
                BeginTranspos(ref block);
                MainEncoding(ref block, key);
                EndTranspos(ref block);

                //вывод результата
                PrintToFile(block, file, true);
            }
        }
        public void decoding_ECB(byte[] key, byte[] content, string outfile)
        {
            //чтение содержимого файла

            byte[] block = { };
            int countOfBlocksdec = (content.Length % 8 != 0) ? content.Length / 8 + 1 : content.Length / 8;

            //чтение блока 64 бита
            for (int i = 0; i < countOfBlocksdec; i++)
            {
                block = InputBlock(content, i);
                BeginTranspos(ref block);
                MainDecoding(ref block, key);
                EndTranspos(ref block);
                PrintToFile(block, outfile, true);
            }
        }
        // конец ECB:


        // ----CBC:
        /// <summary>
        ///     CBC - 
        /// </summary>
        /// <param name="content">последовательность входных блоков</param>
        /// <param name="key">ключ шифрования</param>
        public void encoding_CBC(string file, byte[] content, byte[] key) 
        {
            byte[] v1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0};

            byte[] c_i_1 = v1;

            int countOfBlocks = (content.Length % 8 != 0) ? content.Length / 8 + 1 : content.Length / 8;
            byte[] block = { };

            for (int i = 0; i < countOfBlocks; i++)
            {
                //чтение блока 64 бита
                block = InputBlock(content, i);

                block = XOR(block, c_i_1);

                //начальная перестановка блока
                BeginTranspos(ref block);

                //16 раундов шифрования
                MainEncoding(ref block, key);

                //конечная перестановка блока
                EndTranspos(ref block);

                c_i_1 = block;

                //вывод результата
                PrintToFile(block, file, true);
            }
        }
        public void decoding_CBC(byte[] key, byte[] content, string outfile) 
        {
            byte[] v1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0};

            byte[] c_i_1 = v1; // предыдущий зашифрованный блок
            byte[] c_i = v1;   // текущий зашифрованный блок


            byte[] block = { };
            int countOfBlocksdec = (content.Length % 8 != 0) ? content.Length / 8 + 1 : content.Length / 8;

            //чтение блока 64 бита
            for (int i = 0; i < countOfBlocksdec; i++)
            {
                block = InputBlock(content, i);

                c_i = block; // текущий зашифрованный блок будет использоваться для расшифровки следущего

                BeginTranspos(ref block);
                MainDecoding(ref block, key);
                EndTranspos(ref block);

                block = XOR(block, c_i_1);  // хор с прошлым зашифрованным блоком
                c_i_1 = c_i;

                PrintToFile(block, outfile, true);
            }
        }
        // конец CBC:


        // ----CFB:
        /// <summary>
        ///     CFB - 
        /// </summary>
        /// <param name="content">последовательность входных блоков</param>
        /// <param name="key">ключ шифрования</param>
        public void encoding_CFB(string file, byte[] content, byte[] key, int t) 
        {
            int countOfBlocks = ((content.Length * 8) % t != 0) ? (content.Length * 8) / t + 1 : (content.Length * 8) / t;
            byte[] block = { };

            byte[] v1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0};

            byte[] p_i_1 = v1;

            for (int i = 0; i < countOfBlocks; i++)
            {
                //чтение блока t бит
                block = InputBlock2(content, i, t);

                // DES p_i_1 он пойдет на XOR
                BeginTranspos(ref p_i_1);
                MainEncoding(ref p_i_1, key);
                EndTranspos(ref p_i_1);

                block = XOR(block, p_i_1);

                // свдиг входного блока и записывание в конец зашифр блока
                for (int k = 0; k < 64-t; k++)
                    p_i_1[k] = p_i_1[k + t];
                for (int j = 0; j < t; j++)
                    p_i_1[j + 64 - t] = block[j];

                //вывод результата
                PrintToFile2(block, file, i, t, true);
            }
        }
        public void decoding_CFB(byte[] key, byte[] content, string outfile, int t) 
        {
            byte[] v1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0};

            // получаем С0
            byte[] p_i_1 = v1;
            BeginTranspos(ref p_i_1);
            MainEncoding(ref p_i_1, key);
            EndTranspos(ref p_i_1);

            byte[] p_i;

            byte[] block = { };
            int countOfBlocksdec = ((content.Length * 8) % t != 0) ? (content.Length * 8) / t + 1 : (content.Length * 8) / t;

            //чтение блока t бита 
            for (int i = 0; i < countOfBlocksdec; i++)
            {
                block = InputBlock2(content, i, t);

                // сохраняем зашифрованный блок
                p_i = block;

                // расшифровываем текущий блок
                block = XOR(block, p_i_1);

                // свдиг входного блока и записывание в конец зашифр блока
                for (int k = 0; k < 64 - t; k++)
                    p_i_1[k] = p_i_1[k + t];
                for (int j = 0; j < t; j++)
                    p_i_1[j + 64 - t] = p_i[j];

                // DES p_i_1 он пойдет на XOR в следущей итерации
                BeginTranspos(ref p_i_1);
                MainEncoding(ref p_i_1, key);
                EndTranspos(ref p_i_1);
                PrintToFile2(block, outfile, i, t, true); // печатаем t из t бит
            }
        }
        // конец CFB:


        // ----OFB:
        /// <summary>
        ///     ECB - 
        /// </summary>
        /// <param name="content">последовательность входных блоков</param>
        /// <param name="key">ключ шифрования</param>
        public void encoding_OFB(string file, byte[] content, byte[] key, int t) 
        {
            int countOfBlocks = ((content.Length * 8) % t != 0) ? (content.Length * 8) / t + 1 : (content.Length * 8) / t;
            byte[] block = { };

            byte[] v1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0};

            byte[] p_i_1 = v1;
            byte[] in_block = p_i_1;

            for (int i = 0; i < countOfBlocks; i++)
            {
                //чтение блока t бит
                block = InputBlock2(content, i, t);

                // DES p_i_1 он пойдет на XOR
                BeginTranspos(ref p_i_1);
                MainEncoding(ref p_i_1, key);
                EndTranspos(ref p_i_1);

                // шифруем данные
                block = XOR(block, p_i_1);

                // свдиг входного блока и записывание в конец зашифр блока
                for (int k = 0; k < 64 - t; k++)
                    in_block[k] = in_block[k + t];
                for (int j = 0; j < t; j++)
                    in_block[j + 64 - t] = p_i_1[j];

                p_i_1 = in_block;
                // этот входной блок p_i_1 пойдет для шифрования на следующуй итерации

                //вывод результата
                PrintToFile2(block, file, i, t, true);
            }
        }
        public void decoding_OFB(byte[] key, byte[] content, string outfile, int t) 
        {
            byte[] v1 = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0};

            // получаем С0
            byte[] p_i_1 = v1;
            byte[] in_block = v1;
            BeginTranspos(ref p_i_1);
            MainEncoding(ref p_i_1, key);
            EndTranspos(ref p_i_1);

            byte[] p_i;

            byte[] block = { };
            int countOfBlocksdec = ((content.Length * 8) % t != 0) ? (content.Length * 8) / t + 1 : (content.Length * 8) / t;

            //чтение блока t бита 
            for (int i = 0; i < countOfBlocksdec; i++)
            {
                block = InputBlock2(content, i, t);

                // сохраняем зашифрованный блок
                p_i = p_i_1;

                // расшифровываем текущий блок
                block = XOR(block, p_i_1);

                // свдиг входного блока и записывание в конец зашифр блока
                for (int k = 0; k < 64 - t; k++)
                    in_block[k] = in_block[k + t];
                for (int j = 0; j < t; j++)
                    in_block[j + 64 - t] = p_i[j];

                p_i_1 = in_block;

                // DES p_i_1 он пойдет на XOR в следущей итерации
                BeginTranspos(ref p_i_1);
                MainEncoding(ref p_i_1, key);
                EndTranspos(ref p_i_1);
                PrintToFile2(block, outfile, i, t, true); // печатаем t из t бит
            }
        }
        // конец OFB:


        public byte[] InputBlock2(byte[] content, int pos, int t) // на выходе t бит блока № pos, остальное 0. итого блок 64 бита
        {
            byte[] block = new byte[t];
            byte sym = 0;

            int i = (pos * t) / 8;
            int j = (pos * t) % 8;

            for (int k = 0; k < t; )
            {
                if ( i < content.Length)
                    sym = (content[i]);   // очередной символ из блока pos
                else sym = 0;

                // переписываем биты символа в массив ("а" = 97 = 0110 0001)
                for (; (j < 8) && (k < t); j++) //8 т.к. 1 байт = 8 бит
                {
                    block[k] = (byte)((sym >> (7 - j)) % 2);
                    k++;
                }
                j = 0;
                i++;
            }

            return block;
        }
        public void PrintToFile2(byte[] block, string outfile, int pos, int t, bool toChar)
        {
            int i = (pos * t) / 8;
            int j = (pos * t) % 8;

            //печать блока из t битов по байтам
            var mode = FileMode.Create;
            if (File.Exists(outfile))
                mode = FileMode.Append;
            using (var stream = File.Open(outfile, mode))
            {
                BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false);
                if (toChar)
                {
                    for (int k = 0; k < t;)
                    {
                        for (; (j < 8) && (k < t); j++)
                        {
                            ostatok_byte = (byte)((ostatok_byte << 1) + block[k]);
                            k++;    
                        }
                        if (j == 8)
                        {
                            j = 0;
                            writer.Write(ostatok_byte);
                            ostatok_byte = 0;
                        }
                    }
                    return;
                }
                writer.Write(block);
            }
        }
    }
}