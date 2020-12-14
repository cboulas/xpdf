using System.Threading;
using System.Threading.Tasks;

namespace Xpdf
{
    public static class XPdf
    {
        public delegate void FinishEvent();
        public static event FinishEvent Finish;

        private static Task currentThread;
        private static CancellationToken token;
        private static CancellationTokenSource source;

        public static bool InProgress
        {
            /*get => (currentThread != null
                ? currentThread.Status == TaskStatus.Running
                : false);*/
            get;
            private set;
        }

        public static void FromURL(string url, string filename)
        {
            if (InProgress)
                return;

            InProgress = true;
            source = new CancellationTokenSource();
            token = source.Token;
            
            currentThread = Task.Factory.StartNew(() => {
                private_FromURL(url, filename); }, token);
        }

        public static void FromHTML(string html, string filename)
        {
            if (InProgress)
                return;

            InProgress = true;
            source = new CancellationTokenSource();
            token = source.Token;

            currentThread = Task.Factory.StartNew(() => {
                private_FromHTML(html, filename); }, token);
        }

        private static void private_FromURL(string url, string filename)
        {
            new Xpdf.XPdfer().UrlToPdf(new System.Security.Policy.Url(url), filename);

            _finished();
            if (Finish != null)
                Finish();
        }

        private static void private_FromHTML(string html, string filename)
        {
            new Xpdf.XPdfer().HtmlToPdf(html, filename);

            _finished();
            if (Finish != null)
                Finish();
        }

        private static void _finished()
        {
            if (currentThread == null)
                return;

            InProgress = false;
            source.Cancel(true);
        }
    }
}
