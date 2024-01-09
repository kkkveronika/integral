// Практическая работа №1. 
// Вычислить интеграл функции ax^2 + bx + c методом трапеций и методом парабол.
// Студент группы 414, Вариант 8, Кондиляброва Вероника Данииловна. 2023 год
using System.Data.Common;
using System.IO;
using System.Numerics;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

public class find_integral
{
    static int Input_menu()
    {
        bool inputSuccess = false;
        int input = 0;
        while (!inputSuccess)
        {
            string consoleInput = Console.ReadLine();
            if (!int.TryParse(consoleInput, out input))
            {
                Console.WriteLine(
                "Значение должно быть числом.");
                continue;
            }
            inputSuccess = true;
        }
        return input;
    }
    static int Input_choice()
    {
        bool inputSuccess = false;
        int input = 0;
        while (!inputSuccess)
        {
            string consoleInput = Console.ReadLine();
            if (!int.TryParse(consoleInput, out input))
            {
                Console.WriteLine(
                "Значение должно быть числом.");
                continue;
            }
            if (input > 2 || input < 1)
            {
                Console.WriteLine(
                "Попробуйте еще раз:");
                continue;
            }
            inputSuccess = true;
        }
        return input;
    }

    static double Input_a()
    {
        bool inputSuccess = false;
        double input = 0;
        while (!inputSuccess)
        {
            string consoleInput = Console.ReadLine();
            if (!double.TryParse(consoleInput, out input))
            {
                Console.WriteLine(
                "Число a должно быть числом.");
                Console.Write("a = ");
                continue;
            }
            if (input == 0)
            {
                Console.WriteLine("Число a не может быть нулём.");
                Console.Write("a = ");
                continue;
            }
            inputSuccess = true;
        }
        return input;
    }
    static double Input_coef()
    {
        bool inputSuccess = false;
        double input = 0;
        while (!inputSuccess)
        {
            string consoleInput = Console.ReadLine();
            if (!double.TryParse(consoleInput, out input))
            {
                Console.WriteLine(
                "Значение должно быть числом.");
                continue;
            }
            inputSuccess = true;
        }
        return input;
    }

    static double Input_limits(double x)
    {
        bool inputSuccess = false;
        double x2 = 0;
        while (!inputSuccess)
        {
            Console.Write("x2 = ");
            string Input_x2 = Console.ReadLine();
            if (!double.TryParse(Input_x2, out x2))
            {
                Console.WriteLine(
                "Число x2 должно быть числом.");
                continue;
            }
            x2 = double.Parse(Input_x2);
            if (x2 <= x)
            {
                Console.WriteLine(
               "Верхняя граница x2 должна быть больше нижней границы x1.");
                continue;
            }
            inputSuccess = true;
        }
        return x2;
    }
    public static void Main()
    {
        Console.WriteLine("Кондиляброва Вероника\n414 группа\nВариант 8\nПрарктическая работа 1\nВычислить интеграл заданной функции методом трапеций и методом парабол.");


        bool get_result=true;
        const double e = 0.001;
        const int n = 10;
        double a = 0;
        double b = 0;
        double c = 0;
        double x1 = 0;
        double x2 = 0;

        int end = 0;
        while (end != 1)
        {
            Console.WriteLine("Ввести данные вручную - 1, считать данные из файла - 2, получить значение площади - 3, выйти из программы - 4: ");
            int user_choice = Input_menu();

            switch (user_choice)
            {
                case (int)Menu.Manual_Input:
                    {

                        Console.WriteLine("Введите коэффициенты уравнения параболы: ");
                        Console.Write("a = ");
                        a = Input_a();

                        Console.Write("b = ");
                        b = Input_coef();

                        Console.Write("c = ");
                        c = Input_coef();

                        Console.WriteLine("Введите границы параболы: ");
                        Console.Write("x1 = ");
                        x1 = Input_coef();

                        x2 = Input_limits(x1);

                        Save_initial_data(a, b, c, x1, x2);

                    }
                    break;
                case (int)Menu.File_Input:
                    {
                        Console.Write("Введите название файла: ");
                        //string s;
                        string path = Console.ReadLine();
                        StreamReader f = new StreamReader(path);
                        if (!f.EndOfStream)
                        {
                            string s = f.ReadLine();
                            a = double.Parse(s);
                            // что-нибудь делаем с прочитанной строкой s
                        }
                        if (!f.EndOfStream)
                        {
                            string s = f.ReadLine();
                            b = double.Parse(s);
                            // что-нибудь делаем с прочитанной строкой s
                        }
                        if (!f.EndOfStream)
                        {
                            string s = f.ReadLine();
                            c = double.Parse(s);
                            // что-нибудь делаем с прочитанной строкой s
                        }
                        else
                        {
                            get_result = false;
                        }
                        if (!f.EndOfStream)
                        {
                            string s = f.ReadLine();
                            x1 = double.Parse(s);
                            // что-нибудь делаем с прочитанной строкой s
                        }
                        else
                        {
                            get_result = false;
                        }
                        if (!f.EndOfStream)
                        {
                            string s = f.ReadLine();
                            x2 = double.Parse(s);
                            if (x2 <= x1)
                            {
                                Console.WriteLine("Верхняя граница x2 должна быть больше нижней границы x1.");
                                get_result = false;
                            }
                            // что-нибудь делаем с прочитанной строкой s
                        }
                        else
                        {
                            get_result = false;
                        }
                        if (a != 0 && b != 0 && get_result != false) { Console.WriteLine("Значения считаны."); }
                        else
                        {
                            Console.WriteLine("Файл заполнен неверно.");
                        }
                        f.Close();
                    }
                    break;
                case (int)Menu.Get_result:
                    {
                        if(a==0 && b==0)
                        {
                            Console.WriteLine("Сначала введите данные.");
                            break;
                        }
                        if(get_result== false)
                        {
                            Console.WriteLine("Файл заполнен неверно.");
                            break;
                        }
                        if (Math.Abs(trapezoid_int(a, b, c, x1, x2, n) - parabola_int(a, b, c, x1, x2, n)) > e)
                        {
                            if (Math.Abs(trapezoid_int(a, b, c, x1, x2, n * 10) - parabola_int(a, b, c, x1, x2, n * 10)) > e)
                            {
                                Console.WriteLine("Максимально приближенное значение площади с количеством отрезков 1000: ");
                                
                                Console.Write("Метод трапеций: ");
                                Console.WriteLine(trapezoid_int(a, b, c, x1, x2, n * 100));

                                Console.Write("Метод парабол: ");
                                Console.WriteLine(parabola_int(a, b, c, x1, x2, n * 100));
                                Save_f(a, b, c, x1, x2, n * 100);
                            }
                            else
                            {
                                Console.WriteLine("Максимально приближенное значение с количеством отрезков 100: ");

                                Console.Write("Метод трапеций: ");
                                Console.WriteLine(trapezoid_int(a, b, c, x1, x2, n * 10));

                                Console.Write("Метод парабол: ");
                                Console.WriteLine(parabola_int(a, b, c, x1, x2, n * 10));
                                Save_f(a, b, c, x1, x2, n * 10);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Максимально приближенное значение с количеством отрезков 10: ");
                            Console.Write("Метод трапеций: ");
                            Console.WriteLine(trapezoid_int(a, b, c, x1, x2, n));

                            Console.Write("Метод парабол: ");
                            Console.WriteLine(parabola_int(a, b, c, x1, x2, n));
                            Save_f(a, b, c, x1, x2, n);
                        }
                    }
                    break;
                case (int)Menu.Exit:
                    {
                        end = 1;
                    }
                    break;
                default:
                    Console.WriteLine(
                    "Попробуйте еще раз.");
                    break;
            }
        }
    }

    static void Save_initial_data(double a, double b, double c, double x1, double x2)
    {
        Console.WriteLine("Сохранить в файл? Да - 1, Нет - 2");
        int choice_save = Input_choice();

        if (choice_save == 1)
        {
            string path = "";
            Console.WriteLine("Добавить в существующий файл или создать новый - 1, Перезаписать существующий файл - 2");
            choice_save = Input_choice();
            if (choice_save == 1)
            {

                string str_a = a.ToString();
                string str_b = b.ToString();
                string str_c = c.ToString();
                string str_x1 = x1.ToString();
                string str_x2 = x2.ToString();
                string initial_data = str_a + "\n" + str_b + "\n" + str_c + "\n" + str_x1 + "\n" + str_x2;

                Console.WriteLine("Введите название");
                path = Console.ReadLine();
                try
                {

                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.Write(initial_data);
                    }
                    Console.WriteLine("Сохранено");
                }
                catch
                {
                    Console.WriteLine("Данное название не подходит, введите другое: ");
                    path = Console.ReadLine();
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.Write(initial_data);
                    }
                    Console.WriteLine("Сохранено");
                }
            }
            else
            { 
                string str_a = a.ToString();
                string str_b = b.ToString();
                string str_c = c.ToString();
                string str_x1 = x1.ToString();
                string str_x2 = x2.ToString();
                string initial_data = str_a + "\n" + str_b + "\n" + str_c + "\n" + str_x1 + "\n" + str_x2;
                // полная перезапись файла 
                Console.WriteLine("Введите название");
                path = Console.ReadLine();
                try
                {

                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        writer.Write(initial_data);
                    }
                    Console.WriteLine("Сохранено");
                }
                catch
                {
                    Console.WriteLine("Данное название не подходит, введите другое: ");
                    path = Console.ReadLine();
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        writer.Write(initial_data);
                    }
                    Console.WriteLine("Сохранено");
                }
            }
        }
    }
    static void Save_f(double a, double b, double c, double x1, double x2, int n)
    {
        Console.WriteLine("Сохранить в файл? Да - 1, Нет - 2");
        int choice_save = Input_choice();

        if (choice_save == 1)
        {
            Console.WriteLine("Добавить в существующий файл или создать новый - 1, Перезаписать существующий файл - 2");
            choice_save = Input_choice();
            if (choice_save == 1)
            {
                string integ = trapezoid_int(a, b, c, x1, x2, n).ToString();
                integ += "\n";
                integ += parabola_int(a, b, c, x1, x2, n).ToString();
                // добавление в файл
                Console.WriteLine("Введите название");
                string path = Console.ReadLine();
                try
                {

                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.Write(integ);
                    }
                    Console.WriteLine("Сохранено");
                }
                catch
                {
                    Console.WriteLine("Данное название не подходит, введите другое: ");
                    path = Console.ReadLine();
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.Write(integ);
                    }
                    Console.WriteLine("Сохранено");
                }
            }
            else
            {
                Console.Write("Введите название файла: ");
                string path = Console.ReadLine();
                string integ = trapezoid_int(a, b, c, x1, x2, n).ToString();
                integ += "\n";
                integ += parabola_int(a, b, c, x1, x2, n).ToString();
                // полная перезапись файла 
                try
                {
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        writer.Write(integ);
                    }
                    Console.WriteLine("Сохранено");
                }
                catch
                {
                    Console.WriteLine("Данное название не подходит, введите другое: ");
                    path = Console.ReadLine();
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        writer.Write(integ);
                    }
                    Console.WriteLine("Сохранено");
                }
            }
        }
    }
    public static double trapezoid_int(double a, double b, double c, double x1, double x2, int n)
    {
        //n=10
        //I=h*( (x10 - x0)/2 + (x1 + x2 +...+ x9)
        double x = 0;
        double func_sum = 0;
        double I = 0;
        for (int i = 1; i < n; i++)
        {
            x = x + (x2 - x1) / n;
            func_sum = a * (double)Math.Pow(x, 2) + b * x + c + func_sum;
        }
        double func_x1 = 0;
        func_x1 = a * (double)Math.Pow(x1, 2) + b * x1 + c;

        double func_x2 = 0;
        func_x2 = a * (double)Math.Pow(x2, 2) + b * x2 + c;

        I = (x2 - x1) / n * (((func_x2 + func_x1) / 2) + func_sum);
        return Math.Abs(I);
    }

    public static double parabola_int(double a, double b, double c, double x1, double x2, int n)
    {
        //n=10
        //I=h/3*(x0 + x10 + 2 * (x2 + x4 + x6 + x8) + 4 * (x1 + x3 + x5 + x7 + x9)
        double x_even = (x2 - x1) / n;
        double x_odd = 2 * (x2 - x1) / n;
        double func_odd = 0;
        double func_even = 0;
        double I = 0;
        for (int i = 0; i < n / 2; i++)
        {
            func_even = a* (double)Math.Pow(x_even, 2) + b * x_even + c + func_even;
            x_even = x_even + 2 * (x2 - x1) / n;
        }

        for (int i=1; i < n / 2; i++)
        {
            func_odd = a * (double)Math.Pow(x_odd, 2) + b * x_odd + c + func_odd;
            x_odd = x_odd + 2 * (x2 - x1) / n;
        }
        double h = 0;
        h = (x2 - x1) / (n * 3);

        double func_x1 = 0;
        func_x1 = a * (double)Math.Pow(x1, 2) + b * x1 + c;

        double func_x2 = 0;
        func_x2 = a * (double)Math.Pow(x2, 2) + b * x2 + c;

        I = h * (func_x1 + func_x2 + 2 * func_odd + 4 * func_even);
        return Math.Abs(I);
    }

    enum Menu : int
    {
        Manual_Input = 1,
        File_Input,
        Get_result,
        Exit
    }
}
