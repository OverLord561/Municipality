using System;
using System.Threading;
using System.Threading.Tasks;

namespace MLS
{
    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {

            MLS();

            Console.ReadLine();

        }

        public async static void MLS()
        {
            (double T1, double T2, double MIN_SUM) res = (0, 0, 10000);

            Task.Factory.StartNew(() =>
            {
                for (var thousand = 0; thousand < 1; thousand++)
                    Task.Factory.StartNew(() =>
                    {
                        int K0 = 1;
                        double[] time = new double[12] { 0, 0.0027, 0.0058, 0.0083, 0.0088, 0.0138, 5, 13.3, 16.6, 33.33, 39.166, 0.0054 };
                        double[] y = new double[12] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 0.1 };

                        for (double i =1; i < 1000 * thousand + 1; i++)
                        {


                            for (double j = 1 ; j < 1000 * thousand + 1; j++)
                            {
                                
                                double t1 = i;
                                double t2 = j;


                                double sum = 0;

                                for (int index = 0; index < time.Length; index++)
                                {
                                    double first_part = K0 * (1 - ((t1 / (t1 - t2)) * Math.Exp(-time[index] / t1)) + ((t2 / (t1 - t2)) * Math.Exp(-time[index] / t2)));
                                    double second_part = y[index];
                                    sum += Math.Pow((first_part - second_part), 2);
                                }
                                lock (locker)
                                {
                                    if (sum < res.MIN_SUM)
                                    {
                                        res.MIN_SUM = sum;
                                        res.T1 = t1;
                                        res.T2 = t2;
                                    }
                                }

                            }

                        }


                    },
                       TaskCreationOptions.AttachedToParent);
            }).Wait();



            Console.WriteLine(res);
            Console.ReadLine();

        }

        //public class State
        //{
        //    public int Thousand;
        //    public double T1;
        //    public double T2;
        //    public double MIN_SUM;
        //}

        //public static void Method(object state)
        //{
        //    var a = state as State;

        //    int K0 = 1;
        //    double[] time = new double[11] { 0, 0.0027, 0.0058, 0.0083, 0.0088, 0.0138, 5, 13.3, 16.6, 33.33, 39.166 };
        //    double[] y = new double[11] { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1 };

        //    for (int i = 1; i < 100 * a.Thousand + 1; i++)
        //    {


        //        for (int j = 2; j < 100 * a.Thousand + 1; j++)
        //        {
        //            double t1 = i;
        //            double t2 = j;


        //            double sum = 0;

        //            for (int index = 0; index < time.Length; index++)
        //            {
        //                double first_part = K0 - (1 - t1 / (t1 - t2) * Math.Exp(-time[index] / t1) + t2 / (t1 - t2) * Math.Exp(-time[index] / t2));
        //                double second_part = y[index];
        //                sum += Math.Pow((first_part - second_part), 2);
        //            }
        //            if (sum < a.MIN_SUM)
        //            {
        //                a.MIN_SUM = sum;
        //                a.T1 = t1;
        //                a.T2 = t2;
        //            }

        //        }

        //    }
        //}


        //public static Task Test1(State state)
        //{

        //    return Task.Factory.StartNew(Method, state);
        //}




    }
}
