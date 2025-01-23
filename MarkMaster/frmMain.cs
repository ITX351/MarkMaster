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
            crawler.OnProgressChanged += UpdateProgress;
            await Task.Run(crawler.StartCrawl);
            UpdateProgress("资源下载完成！");
        }

        private void UpdateProgress(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateProgress), message);
            }
            else
            {
                lblLoadingStatus.Text = message;
            }
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

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpMessage = "MarkMaster 功能说明：\n\n" +
                                 "1. 启动爬虫：点击主界面的“更新技能烙痕资源”按钮，系统将依次抓取技能、记忆烙痕和角色数据，并下载相关图片资源。\n" +
                                 "2. 查看技能：点击“传承技能一览”按钮，打开技能查看界面。\n" +
                                 "   - 鼠标悬停：展示技能所属的烙痕和相关队长技。加粗的烙痕为拥有“技能解锁提升”效果的烙痕，右侧的数值为它的概率。\n" +
                                 "   - 技能筛选：支持按技能类型、等级和名称进行筛选。\n" +
                                 "   - 编辑模式：点击左上角的按钮进入编辑模式，左键提高技能等级，右键降低技能等级。单击保存会记录所有技能的等级，等级会存储到本地json文件中。\n" +
                                 "   - 技能标签：使用“CTRL/ALT/SHIFT+左键/右键”的组合键可以对技能进行分类标签，一共六种标签，分别对应不同的颜色。再点一次相同的组合键可以取消标注。标注会存储到本地json文件中。\n" +
                                 "   - 漫巡烙痕选择：非编辑模式下，左键单击技能会将技能加入下方的候选列表，再次单击（或右键单击下方列表中的候选技能列头）可以取消之。依次展示所有技能的烙痕，重复的烙痕会用方框圈出来。漫巡过程中可以优先选择方框圈出的重复的烙痕，以及拥有“技能解锁提升”效果的烙痕。\n" +
                                 "3. 导入数据：点击“导入数据”按钮，打开数据导入界面。\n" +
                                 "   - 输入技能名，每行一个，结尾可以用空格+1/2的数字来标注等级。未标注等级的技能视为3级。成功匹配的技能对应的行会被删除，所有未成功匹配的技能的文本会保留在框内，你可以更改文本，或回到技能展示页面使用编辑功能以手动更改匹配失败的技能名。";
            MessageBox.Show(helpMessage, "帮助", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
