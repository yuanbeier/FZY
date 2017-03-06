#if TEST
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Topevery.FMP.Data.Common.Test
{

    [TestFixture]
    public class DbHelperTest
    {
        [Test]
        public void CreateDatabase()
        {
            ThreadStart tStart = new ThreadStart(ThreadStartInvoke);
            Thread thread = new Thread(tStart);
            Thread thread1 = new Thread(tStart);
            thread1.Start();
            thread.Start();
        }

        void ThreadStartInvoke()
        {
            int Max = 50000;
            System.Diagnostics.Stopwatch w = new Stopwatch();
            w.Reset();
            w.Start();

            for (int i = 0; i < Max; i++)
            {
                //Database data = DatabaseFactory.CreateDatabase(); 
                Database data = DbHelper.GetDatabase("fmpDatabase");
            }
            w.Stop();
            Console.WriteLine("Execute Time:" + w.ElapsedMilliseconds.ToString());
        }
    }

}
#endif
