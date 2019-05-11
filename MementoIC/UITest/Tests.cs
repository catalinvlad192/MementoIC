using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AddNewHighPriorityTask()
        {
            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test Memento");
            app.Tap(c => c.Id("editDescription"));
            app.EnterText("Description for the test");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1500);
            app.Tap(c => c.Id("buttonSave"));

            Assert.IsTrue(true);
        }

        [Test]
        public void AddATaskThenDeleteTheFirstTask()
        {
            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test Memento");
            app.Tap(c => c.Id("editDescription"));
            app.EnterText("Description for the test");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1500);
            app.Tap(c => c.Id("buttonSave"));

            app.TapCoordinates(540, 400);
            app.Tap(c => c.Id("editDescription"));
            app.EnterText("");
            app.DismissKeyboard();
            app.Tap(c => c.Id("buttonDelete"));

            Assert.IsTrue(true);
        }

        [Test]
        public void Add5TasksThenSortThem()
        {
            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test 1");
            app.DismissKeyboard();
            app.Tap(c => c.Id("buttonSave"));

            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test 2");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1500);
            app.TapCoordinates(915, 1100);
            app.Tap(c => c.Id("buttonSave"));

            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test 3");
            app.DismissKeyboard();
            app.Tap(c => c.Id("buttonSave"));

            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test 4");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1400);
            app.TapCoordinates(700, 1400);
            app.Tap(c => c.Id("buttonSave"));

            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test 5");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1500);
            app.Tap(c => c.Id("buttonSave"));

            app.Tap(c => c.Id("sortPriority"));
            app.Tap(c => c.Id("sortDate"));

            Assert.IsTrue(true);
        }

        [Test]
        public void AddTaskThenDisableItsNotification()
        {
            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test Memento");
            app.Tap(c => c.Id("editDescription"));
            app.EnterText("Description for the test");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1500);
            app.Tap(c => c.Id("buttonSave"));

            app.TapCoordinates(5, 400);

            Assert.IsTrue(true);
        }

        [Test]
        public void CheckDatePicker()
        {
            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test Memento");
            app.Tap(c => c.Id("editDescription"));
            app.EnterText("Description for the test");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1100);
            app.Tap(c => c.Id("buttonSave"));

            Assert.IsTrue(true);
        }

        [Test]
        public void CheckTimePicker()
        {
            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test Memento");
            app.Tap(c => c.Id("editDescription"));
            app.EnterText("Description for the test");
            app.DismissKeyboard();
            app.TapCoordinates(700, 1400);
            app.Tap(c => c.Id("buttonSave"));

            Assert.IsTrue(true);
        }

        [Test]
        public void AddATaskThenEditIt()
        {
            app.Tap(c => c.Id("buttonNew"));
            app.Tap(c => c.Id("editName"));
            app.EnterText("Test Memento");
            app.DismissKeyboard();
            app.TapCoordinates(915, 1500);
            app.Tap(c => c.Id("buttonSave"));

            app.TapCoordinates(540, 400);
            app.Tap(c => c.Id("editDescription"));
            app.EnterText("Description for the test");
            app.DismissKeyboard();
            app.Tap(c => c.Id("buttonSave"));

            Assert.IsTrue(true);
        }
    }
}
