using MarkMaster.Models;

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

        private void btnSkills_Click(object sender, EventArgs e)
        {
            var skillsForm = new frmSkills();
            skillsForm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bool isDataLoaded = GlobalData.Instance.LoadData();
            lblLoadingStatus.Text = isDataLoaded ? "本地数据加载成功" : "本地数据加载失败，请检查/重新下载资源文件";
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var importForm = new frmImport();
            importForm.Show();
        }
    }
}
