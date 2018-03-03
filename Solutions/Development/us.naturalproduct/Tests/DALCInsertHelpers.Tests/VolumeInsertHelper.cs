using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using us.naturalproduct.DataTransferObjects;
using us.naturalproduct.DataAccessLogicComponents;

namespace us.naturaproduct.DALCInsertHelpers.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class VolumeInsertHelper
    {
        public VolumeInsertHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        Volume volumeDtoAfterInsert;

        [TestMethod]
        public void TestInsertVolume()
        {
            Volume volumeDto = new Volume();

            volumeDto.VolumeYear = "2008";
            volumeDto.VolumeName = "custom volume name";
            volumeDto.CreationUserId = 2;
            volumeDto.IsActive = true;

            try
            {
                VolumeDALC dalc = new VolumeDALC();

                volumeDtoAfterInsert = dalc.Insert(volumeDto);

                Volume volumeDtoAfterSelect = dalc.Select(volumeDtoAfterInsert);

                Assert.AreEqual(volumeDto.VolumeName, volumeDtoAfterSelect.VolumeName, "VolumeName");
                Assert.AreEqual(volumeDto.VolumeYear, volumeDtoAfterSelect.VolumeYear, "VolumeYear");
                Assert.AreEqual(volumeDto.IsActive, volumeDtoAfterSelect.IsActive, "IsActive");
                Assert.AreEqual(volumeDto.CreationUserId, volumeDtoAfterSelect.CreationUserId, "CreationUserId");
                Assert.AreEqual(volumeDto.CreationUserId, volumeDtoAfterSelect.UpdateUserId, "UpdateUserId");
                Assert.AreEqual(volumeDtoAfterSelect.CreationDateTime, volumeDtoAfterSelect.UpdateDateTime, "Create/Update DateTime");               
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

        }

        [TestCleanup]
        public void TestInsertVolumeCleanup()
        {
            try
            {
                VolumeDALC dalc = new VolumeDALC();

                Int32 status = dalc.Delete(volumeDtoAfterInsert);

                Assert.AreEqual(0, status, "Delete failed");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
