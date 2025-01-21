namespace MarkMaster
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private async void btnCrawler_Click(object sender, EventArgs e)
        {
            var crawler = new Scripts.Crawler();
            await Task.Run(crawler.StartCrawl);
        }
    }
}
