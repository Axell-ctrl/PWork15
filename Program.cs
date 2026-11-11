//**************************************************************************************************************************************************************
//*Практическая работа №15                                                                                                                                     *
//*Сделал Егоров Н.Н, группа 2-ИСП                                                                                                                             *
//*Задание: Заполнить файл n числами [-5.00, 10.11]. Вывести содержимое файла на экран, найти наименьший чётный элемент m, увеличить на m исходные значения.   *
//**************************************************************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Практическая_работа__15
{
    internal class Program
    {
        static void HandleError(FormatException fex)//функция с сообщением об исключении FormatException (входная строка имела неправильный формат)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Что-то пошло не так! Ошибка: {fex.Message} Попробуйте ещё раз.");//Вывод текста с помощью интерполяции: Что-то пошло не так! Ошибка: Входная строка имела неправильный формат. 
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void HandleError(OverflowException ofex)//функция с сообщением об исключении OverflowException (Значение было недопустимо малым или недопустимо большим для Int32)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Что-то пошло не так! Ошибка: {ofex.Message} Попробуйте ещё раз.");//Вывод текста с помощью интерполяции: Что-то пошло не так! Ошибка: Значение было недопустимо малым или недопустимо большим для Int32.
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void HandleError(Exception ex)//функция с сообщением об исключении Exception (все ошибки в целом)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Что-то пошло не так! Ошибка: {ex.Message} Попробуйте ещё раз.");//Вывод текста с помощью интерполяции: Что-то пошло не так! Ошибка: сообщение об ошибке.
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void HandleError(IOException IOex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Что-то пошло не так! Ошибка: {IOex.Message} Попробуйте ещё раз.");//Вывод текста с помощью интерполяции: Что-то пошло не так! Ошибка: сообщение об ошибке.
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void WriterFile(uint count, string filepath)
        {
            Random rnd = new Random();

            FileStream f = new FileStream(filepath, FileMode.Create);//создание файла
            StreamWriter writer = new StreamWriter(f);//открытие потока для записи текста

            for (int i = 0; i < count; i++)//
            {
                double number = rnd.Next(-500, 1012) / 100.0;//генерация чисел в диапазоне от 5.00 до 10.11
                writer.Write(number + " ");//запись в файл через пробел
            }
            writer.Close();//закрытие потока
        }
        static void ReaderFile(uint count, string filepath)
        {
            double[] arrRead = new double[count];//массив, количество элементов которого зависит от введённого числа пользователем (count)
            string tempStr;//переменная для хранения считанной с файла строки
            double min = 0;//переменная наименьшего чётного элемента m       
            bool found = false;//логическая переменная, которая показывает найден ли наименьший чётный элемент   
            Console.WriteLine($"--- Содержимое файла {filepath} ---");

            FileStream ff = new FileStream(filepath, FileMode.Open);//открытие файла по указанному пути
            StreamReader reader = new StreamReader(ff);//создание объекта StreamReader для чтения данных из потока

            while ((tempStr = reader.ReadLine()) != null)//цикл до того момента, пока не будет достигнут конец файла
            {
                tempStr = tempStr.Trim();//удаление пробелов в начале и конце строки
                string[] text = tempStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//разделение строки на массив с удалением пустых элементов
                for (int i = 0; i < text.Length; i++)
                {
                    arrRead[i] = Double.Parse(text[i]);// преобразование строкового представления числа в вещественный тип double и запись в массив
                    Console.Write(arrRead[i] + " ");//вывод считанного исходного числа в строку через пробел

                    if ((int)arrRead[i] % 2 == 0)//проверка числа на чётность
                    {
                        if (found == false || arrRead[i] < min)//проверка числа на первое чётное или минимальное чётное
                        {
                            min = arrRead[i];
                            found = true;
                        }
                    }
                }
            }
            reader.Close();//закрытие потока
            Console.WriteLine();

            if (found == true)
            {
                Console.WriteLine($"Минимальное чётное значение m = {min}");
                Console.Write("Числа после увеличения на m: ");
                for (int i = 0; i < arrRead.Length; i++)
                {
                    arrRead[i] += min;
                    Console.Write(arrRead[i] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("В файле нет элементов с чётной целой частью.");
            }
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.Title = "Практическая работа №15";
            Console.WriteLine("Здравствуйте!");
            bool exitProgram = false;

            while (true)
            {
                try
                {
                    Console.Write("Укажите путь сохранения файла: ");
                    string filepath = Console.ReadLine();
                    Console.Write("Введите количество чисел в файле: ");
                    uint count = UInt32.Parse(Console.ReadLine());
                    WriterFile(count, filepath);
                    ReaderFile(count, filepath);
                }
                catch (FormatException fex)
                {
                    HandleError(fex);
                }
                catch (OverflowException ofex)
                {
                    HandleError(ofex);
                }
                catch (IOException IOex)
                {
                    HandleError(IOex);
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                while (true)
                {
                    try
                    {
                        Console.Write("\nХотите продолжить выполнение? (1-Да/0-Нет): ");
                        int answer = Int32.Parse(Console.ReadLine());
                        if (answer < 0 || answer > 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Вы ввели некорректное число. Попробуйте ещё раз.");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                        else
                        {
                            if (answer == 0)
                            {
                                exitProgram = true;
                                Console.WriteLine("Завершение программы. Нажмите для продолжения...");
                            }
                            break;
                        }
                    }
                    catch (FormatException fex)
                    {
                        HandleError(fex);
                    }
                    catch (OverflowException ofex)
                    {
                        HandleError(ofex);
                    }
                    catch (Exception ex)
                    {
                        HandleError(ex);
                    }
                }
                if (exitProgram == true)
                    break;
            }
            Console.ReadKey();
        }
    }
}
