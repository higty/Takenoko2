using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;

namespace BlogWebSiteTest
{
    [TestClass]
    public class UnitTest1
    {
        private string _BaseUrl = "https://takenoko-blog.azurewebsites.net";
        [TestMethod]
        public void PageResponse200()
        {
            _BaseUrl = "https://localhost:7000";

            var dr = new ChromeDriver();
            dr.Navigate().GoToUrl(_BaseUrl);
            dr.Navigate().GoToUrl(_BaseUrl + "/blog/entry/list");
            var pl = dr.FindElement(By.CssSelector(".blog-entry-panel"));
            Assert.IsNotNull(pl);

            dr.Navigate().GoToUrl(_BaseUrl + "/blog/entry/add");
            var bt = dr.FindElement(By.CssSelector("[name='SaveButton']"));
            Assert.IsNotNull(bt);
        }
        [TestMethod]
        public void AddBlogEntry()
        {
            _BaseUrl = "https://localhost:7000";

            var dr = new ChromeDriver();
            dr.Navigate().GoToUrl(_BaseUrl + "/blog/entry/add");
            var tx = dr.FindElement(By.CssSelector("input[name='Title']"));
            var title = "Test Post " + DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
            tx.SendKeys(title);
            Thread.Sleep(3000);
            dr.ExecuteJavaScript("window[\"currentPage\"].textBox.setContent(\"Blog Body1\");");
            var bt = dr.FindElement(By.CssSelector("[name='SaveButton']"));
            bt.Click();
            Thread.Sleep(3000);
            var a = dr.FindElement(By.CssSelector(".blog-entry-panel a"));
            Assert.AreEqual(title, a.Text);
        }
    }
}