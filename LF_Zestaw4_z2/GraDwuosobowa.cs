using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace LF_Zestaw4_z2
{
    public abstract class GraDwuosobowa
    {
        private static int idGen = 0;
        private static List<string> log = new List<string>();

        public static int RozmiarLog { get { return log.Count; } }

        public static string Log(int i)
        {
            return log[i];
        }

        public static void WyczyscLog()
        {
            log.Clear();
            log.TrimExcess();
        }

        private BackgroundWorker worker;

        public abstract string Nazwa { get; }

        public bool JestPrzerywana { get; private set; }
        public int Id { get; private set; }
        public bool Uruchomiona { get { return worker.IsBusy; } }

        public bool Uruchom()
        {
            if (!Uruchomiona)
            {
                JestPrzerywana = false;
                worker.DoWork += this.Event_DoWork;
                worker.ProgressChanged += this.Event_Report;
                worker.RunWorkerCompleted += this.Event_WorkCompleted;

                worker.RunWorkerAsync();
                return true;
            }
            return false;
        }

        public virtual void Przerwij()
        {
            if(Uruchomiona)
                JestPrzerywana = true;
        }

        protected void DodajLog(string msg)
        {
            log.Add("[Gra: " + Nazwa + " " + Id.ToString() + ", Czas: " + DateTime.Now.ToString() + "]: " + msg);
        }

        protected void Uspij(int ms)
        {
            Thread.Sleep(ms);
        }

        protected void Reportuj()
        {
            worker.ReportProgress(0);
        }

        protected virtual void Raport() { }
        protected virtual void KoniecPracy() { }

        protected abstract void UstawGre();
        protected abstract void RozegrajGre();
        protected abstract void GraJestSkonczona();
        protected abstract void WyswietlZwyciezce();

        protected GraDwuosobowa()
        {
            JestPrzerywana = false;
            Id = idGen++;

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
        }

        private void Event_DoWork(object sender, DoWorkEventArgs e)
        {
            DodajLog("Ustawiam gre...");

            UstawGre();

            if (JestPrzerywana) return;
            DodajLog("Gra zostala ustawiona. Rozpoczynamy gre!");

            RozegrajGre();

            if (JestPrzerywana) return;
            DodajLog("Gra zostala ukonczona!");

            GraJestSkonczona();

            if (JestPrzerywana) return;
            DodajLog("Posprzatalem po sobie.");

            WyswietlZwyciezce();
        }

        private void Event_Report(object sender, ProgressChangedEventArgs e)
        {
            Raport();
        }

        private void Event_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DodajLog(JestPrzerywana ? "Gra zostala wylaczona przedwczesnie." : "Juz po wszystkim! :)");

            JestPrzerywana = false;
            worker.DoWork -= this.Event_DoWork;
            worker.ProgressChanged -= this.Event_Report;
            worker.RunWorkerCompleted -= this.Event_WorkCompleted;

            KoniecPracy();
        }
    }
}
