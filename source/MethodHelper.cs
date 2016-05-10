using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftOnline.Ads.BingAdsApi
{
    public class MethodHelper
    {
        internal const int MAX_TRY_COUNT = 3;

        public static TResponse TryGet<TService, TResponse, T1>(Func<TResponse> method, TService service)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method();
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1>(Func<T1, TResponse> method, TService service, T1 t1)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2>(Func<T1, T2, TResponse> method, TService service, T1 t1, T2 t2)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3>(Func<T1, T2, T3, TResponse> method, TService service, T1 t1, T2 t2, T3 t3)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6, t7);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6, t7, t8);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6, t7, t8, t9);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static TResponse TryGet<TService, TResponse, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResponse> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
            where TResponse : class
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            TResponse response = null;
            while (response == null && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static void TryGetVoid<TService>(Action method, TService service)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            while (i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    method();

                    break;
                }
                catch (Exception)
                {
                    if (i == MAX_TRY_COUNT - 1)
                        throw;
                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }
        }

        public static void TryGetVoid<TService, T1>(Action<T1> method, TService service, T1 t1)
           where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            while (i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    method(t1);

                    break;
                }
                catch (Exception)
                {
                    if (i == MAX_TRY_COUNT - 1)
                        throw;
                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }
        }

        public static void TryGetVoid<TService, T1, T2>(Action<T1, T2> method, TService service, T1 t1, T2 t2)
           where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            while (i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    method(t1, t2);

                    break;
                }
                catch (Exception)
                {
                    if (i == MAX_TRY_COUNT - 1)
                        throw;
                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }
        }

        public static void TryGetVoid<TService, T1, T2, T3>(Action<T1, T2, T3> method, TService service, T1 t1, T2 t2, T3 t3)
           where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            while (i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    method(t1, t2, t3);

                    break;
                }
                catch (Exception)
                {
                    if (i == MAX_TRY_COUNT - 1)
                        throw;
                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }
        }

        public static void TryGetVoid<TService, T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4)
           where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            while (i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    method(t1, t2, t3, t4);

                    break;
                }
                catch (Exception)
                {
                    if (i == MAX_TRY_COUNT - 1)
                        throw;
                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }
        }

        public static void TryGetVoid<TService, T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
           where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            while (i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    method(t1, t2, t3, t4, t5);

                    break;
                }
                catch (Exception)
                {
                    if (i == MAX_TRY_COUNT - 1)
                        throw;
                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }
        }

        public static void TryGetVoid<TService, T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
           where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            while (i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    method(t1, t2, t3, t4, t5, t6);

                    break;
                }
                catch (Exception)
                {
                    if (i == MAX_TRY_COUNT - 1)
                        throw;
                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }
        }

        public static bool TryGetBool<TService>(Func<bool> method, TService service)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            bool response = false;
            while (!response && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method();
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static bool TryGetBool<TService, T1>(Func<T1, bool> method, TService service, T1 t1)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            bool response = false;
            while (!response && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static bool TryGetBool<TService, T1, T2>(Func<T1, T2, bool> method, TService service, T1 t1, T2 t2)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            bool response = false;
            while (!response && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static bool TryGetBool<TService, T1, T2, T3>(Func<T1, T2, T3, bool> method, TService service, T1 t1, T2 t2, T3 t3)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            bool response = false;
            while (!response && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static bool TryGetBool<TService, T1, T2, T3, T4>(Func<T1, T2, T3, T4, bool> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            bool response = false;
            while (!response && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static bool TryGetBool<TService, T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, bool> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            bool response = false;
            while (!response && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }

        public static bool TryGetBool<TService, T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, bool> method, TService service, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
            where TService : LogBase
        {
            //turn off error handler
            service.IgnoreError = true;

            int i = 0;
            bool response = false;
            while (!response && i < MAX_TRY_COUNT)
            {
                if (i == MAX_TRY_COUNT - 1)
                    service.IgnoreError = false;

                try
                {
                    response = method(t1, t2, t3, t4, t5, t6);
                }
                catch { System.Threading.Thread.Sleep(1000); }

                i++;
            }

            return response;
        }
    }
}