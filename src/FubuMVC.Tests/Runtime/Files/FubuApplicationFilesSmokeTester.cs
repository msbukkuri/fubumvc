using System;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Runtime.Files;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Tests.Runtime.Files
{
    [TestFixture]
    public class FubuApplicationFilesSmokeTester
    {
        private FubuApplicationFiles theFiles;

        [SetUp]
        public void SetUp()
        {
            FubuApplication.PhysicalRootPath = AppDomain.CurrentDomain.BaseDirectory.ParentDirectory().ParentDirectory();

            theFiles = new FubuApplicationFiles();
        }

        [Test]
        public void find_file()
        {
            theFiles.AssertHasFile("Runtime/Files/Data/a.txt");

            var fubuFile = theFiles.Find("Runtime/Files/Data/a.txt");
            fubuFile.ShouldNotBeNull();

            fubuFile.ReadContents()
                .Trim().ShouldBe("some text from a.txt");
        }

        [Test]
        public void find_file_canonicizes_paths()
        {
            theFiles.AssertHasFile("Runtime\\Files\\Data\\a.txt");

            theFiles.Find("Runtime\\Files\\Data\\a.txt").ReadContents()
                .Trim().ShouldBe("some text from a.txt");
        }

        [Test]
        public void get_application_path_delegates_to_fubumvc_package_facility()
        {
            theFiles.GetApplicationPath().ShouldBe(FubuApplication.GetApplicationPath());
        }
    }
}