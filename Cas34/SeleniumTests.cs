using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Cas34
{
    class SeleniumTests
    {
        // Declare a variable to hold our webdriver reference
        IWebDriver driver;

        string FirstName;
        string LastName;
        string UserName;
        string Email;

        Random rnd = new Random();


        [SetUp]
        // This method is called automatically every time a test is run
        public void Init()
        {
            // Initialize Chrome as our webdriver
            driver = new ChromeDriver();
            FileClass.LogFileName = "C:\\Kurs\\Cas34.log";
        }

        [Test]
        public void TestQASite()
        {
            string number = rnd.Next(1000, 100000).ToString();
            FirstName = "Ime " + number;
            LastName = "Prezime " + number;
            UserName = "username" + number;
            Email = UserName + "@someemail.com";

            int Users = 5;
            int Fails = 0;

            GoToURL("http://test.qa.rs/");

            for (int i = 0; i < Users; i++)
            {
                ClickOnRegisterLink();
                FillRegistrationForm();
                Wait(100);
                IWebElement success = FindElement(By.XPath("//div[contains(@class, 'alert-success') and contains(., '" + FirstName + "') and contains(., 'Uspeh')]"));
                if (success == null)
                {
                    Fails++;
                }
                Wait(100);
            }

            if (Fails > 0)
            {
                Assert.Fail("Fail! " + Fails.ToString() + " users failed to be created.");
            } else
            {
                Assert.Pass("Success! " + Users.ToString() + " users created successfully.");
            }
        }

        [TearDown]
        // This method is called automatically every time a test has finished its run
        public void Destroy()
        {
            // Close our webdriver and release memory
            driver.Close();
        }

        public void ClickOnRegisterLink()
        {
            IWebElement LinkKreiraj = FindElement(By.XPath("//a[contains(., 'Kreiraj')]"));
            LinkKreiraj.Click();
        }

        public void FillRegistrationForm()
        {
            IWebElement ime = FindElement(By.Name("ime"));
            IWebElement prezime = FindElement(By.Name("prezime"));
            IWebElement korisnicko = FindElement(By.Name("korisnicko"));
            IWebElement email = FindElement(By.Name("email"));
            IWebElement telefon = FindElement(By.Name("telefon"));
            IWebElement zemlja = FindElement(By.Name("zemlja"));
            // By.XPath("//input[@name='pol' and @value='m']")
            IWebElement pol_m = FindElement(By.Id("pol_m"));
            // By.XPath("//input[@name='pol' and @value='z']")
            IWebElement pol_z = FindElement(By.Id("pol_z"));
            IWebElement obavestenja = FindElement(By.Name("obavestenja"));
            IWebElement promocije = FindElement(By.Name("promocije"));
            IWebElement register = FindElement(By.Name("register"));
            ime.SendKeys(FirstName);
            prezime.SendKeys(LastName);
            korisnicko.SendKeys(UserName);
            email.SendKeys(Email);
            telefon.SendKeys(rnd.Next(100000, 999999).ToString());
            zemlja.SendKeys("Serbia");

            if (rnd.Next(1, 100) > 50)
            {
                pol_m.Click();
            }
            else
            {
                pol_z.Click();
            }
            if (rnd.Next(1, 100) > 50)
            {
                obavestenja.Click();
            }
            if (rnd.Next(1, 100) > 50)
            {
                promocije.Click();
            }

            register.Click();
        }

        ///<summary>
        ///Wait a specified number of <paramref name="ms" />.
        ///</summary>
        ///<param name="ms">Integer. Number of miliseconds to wait for.</param>
        static private void Wait(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }

        ///<summary>
        ///Navigate to a specified <paramref name="url" />.
        ///Wait specified number of <paramref name="ms"/> before, and after navigating.
        ///</summary>
        ///<param name="url">String. URL to which to navigate to.</param>
        ///<param name="ms">Integer. Number of miliseconds to wait before and after navigating.</param>
        private void GoToURL(string url, int ms = 1000)
        {
            Wait(ms);
            driver.Navigate().GoToUrl(url);
            Wait(ms);
        }

        ///<summary>
        ///Searches for an element usng the <paramref name="criteria" /> provided.
        ///</summary>
        ///<param name="criteria">By. Criteria by which to look for an element.</param>
        public IWebElement FindElement(By selector)
        {
            // Declare a variable to hold our found element
            IWebElement elReturn = null;
            // Try/Catch
            try
            {
                // Try to find an element using Selenium
                elReturn = driver.FindElement(selector);
            }
            catch (NoSuchElementException)
            {
                // If Selenium can't find an element, catch the exception
                // and log it
                // Put a $ in front of double quotes to allow code between { and } to be executed
                // within the string
                string Message = $"Element \"{selector.ToString()}\" not found.";
                FileClass.Log(Message);
            }
            catch (Exception e)
            {
                // Allow other exceptions to pass through our code
                throw e;
            }

            // Return found element
            return elReturn;
        }
    }
}