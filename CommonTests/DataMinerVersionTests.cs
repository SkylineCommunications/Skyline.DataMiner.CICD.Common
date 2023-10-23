namespace CommonTests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Skyline.DataMiner.CICD.Common;

    [TestClass]
    public class DataMinerVersionTests
    {
        #region Constructors + properties

        [TestMethod]
        public void DataMinerVersion_Constructor_WithVersion_WithIteration()
        {
            // Arrange
            Version version = new Version(6, 48, 9, 10);
            const int iteration = 123;
            
            // Act
            DataMinerVersion result = new DataMinerVersion(version, iteration);

            // Assert
            result.Version.Should().BeEquivalentTo(version);
            result.Major.Should().Be(6);
            result.Minor.Should().Be(48);
            result.Build.Should().Be(9);
            result.Revision.Should().Be(10);
            result.Iteration.Should().Be(123);
        }

        [TestMethod]
        public void DataMinerVersion_Constructor_WithVersion()
        {
            // Arrange
            Version version = new Version(6, 48, 9, 10);
            
            // Act
            DataMinerVersion result = new DataMinerVersion(version);

            // Assert
            result.Version.Should().BeEquivalentTo(version);
            result.Major.Should().Be(6);
            result.Minor.Should().Be(48);
            result.Build.Should().Be(9);
            result.Revision.Should().Be(10);
            result.Iteration.Should().Be(0);
        }

        [TestMethod]
        public void DataMinerVersion_Constructor_WithIntArguments()
        {
            // Arrange

            // Act
            DataMinerVersion result = new DataMinerVersion(8, 153, 9);

            // Assert
            result.Version.Should().BeEquivalentTo(new Version(8, 153, 9));
            result.Major.Should().Be(8);
            result.Minor.Should().Be(153);
            result.Build.Should().Be(9);
            result.Revision.Should().Be(-1);
            result.Iteration.Should().Be(0);
        }

        #endregion

        #region TryParse + Parse
        
        [TestMethod]
        [DataRow(null, false)]
        [DataRow("", false)]
        [DataRow("          ", false)]
        [DataRow("InvalidText", false)]
        [DataRow("1", false)]
        [DataRow("1.0", true)]
        [DataRow("1.0.0", true)]
        [DataRow("1.0.0.1", true)]
        [DataRow("1.0.0.1-123", true)]
        [DataRow("1.0.A.1-123", false)]
        [DataRow("1.0.-1.1-123", false)]
        [DataRow("1.0.0.-1", false)]
        public void DataMinerVersion_TryParse_ArgumentValidation(string input, bool expectedResult)
        {
            // Arrange

            // Act
            bool result = DataMinerVersion.TryParse(input, out _);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void DataMinerVersion_TryParse_WithIteration()
        {
            // Arrange
            const string input = "1.0.0.1-123456";

            // Act
            bool result = DataMinerVersion.TryParse(input, out DataMinerVersion resultVersion);

            // Assert
            result.Should().BeTrue();
            resultVersion.Major.Should().Be(1);
            resultVersion.Minor.Should().Be(0);
            resultVersion.Build.Should().Be(0);
            resultVersion.Revision.Should().Be(1);
            resultVersion.Iteration.Should().Be(123456);
        }

        [TestMethod]
        public void DataMinerVersion_TryParse_WithoutIteration()
        {
            // Arrange
            const string input = "1.0.0.1";

            // Act
            bool result = DataMinerVersion.TryParse(input, out DataMinerVersion resultVersion);

            // Assert
            result.Should().BeTrue();
            resultVersion.Major.Should().Be(1);
            resultVersion.Minor.Should().Be(0);
            resultVersion.Build.Should().Be(0);
            resultVersion.Revision.Should().Be(1);
            resultVersion.Iteration.Should().Be(0);
        }

        [TestMethod]
        public void DataMinerVersion_Parse_Null_ExpectedArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => DataMinerVersion.Parse(null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void DataMinerVersion_Parse_Invalid_ExpectedArgumentException()
        {
            // Arrange

            // Act
            Action act = () => DataMinerVersion.Parse("ABC");

            // Assert
            act.Should().ThrowExactly<ArgumentException>().WithMessage("Input 'ABC' is not a valid DataMiner version.*input*");
        }

        #endregion

        #region EqualSign

        [TestMethod]
        public void DataMinerVersion_EqualSign_Success()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3));

            // Act
            bool result = version1 == version2;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DataMinerVersion_EqualSign_Fail()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3), 5);

            // Act
            bool result = version1 == version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_EqualSign_Null_Right()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 == version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_EqualSign_Null_Left()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = new DataMinerVersion(1, 2, 3);

            // Act
            bool result = version1 == version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_EqualSign_Null_Both()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 == version2;

            // Assert
            result.Should().BeTrue();
        }
        
        #endregion

        #region NotEqualSign

        [TestMethod]
        public void DataMinerVersion_NotEqualSign_Success()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3), 5);

            // Act
            bool result = version1 != version2;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DataMinerVersion_NotEqualSign_Fail()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3));

            // Act
            bool result = version1 != version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_NotEqualSign_Null_Right()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 != version2;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DataMinerVersion_NotEqualSign_Null_Left()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = new DataMinerVersion(1, 2, 3);

            // Act
            bool result = version1 != version2;

            // Assert
            result.Should().BeTrue();
        }
        
        [TestMethod]
        public void DataMinerVersion_NotEqualSign_Null_Both()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 != version2;

            // Assert
            result.Should().BeFalse();
        }

        #endregion

        #region LessThanSign

        [TestMethod]
        public void DataMinerVersion_LessThanSign_Success()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3), 5);

            // Act
            bool result = version1 < version2;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanSign_Fail()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3));

            // Act
            bool result = version1 < version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanSign_Null_Right()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 < version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanSign_Null_Left()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = new DataMinerVersion(1, 2, 3);

            // Act
            bool result = version1 < version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanSign_Null_Both()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 < version2;

            // Assert
            result.Should().BeFalse();
        }
        
        #endregion

        #region LessThanOrEqualSign

        [TestMethod]
        public void DataMinerVersion_LessThanOrEqualSign_Success()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3), 5);

            // Act
            bool result = version1 <= version2;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanOrEqualSign_Fail()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 2));

            // Act
            bool result = version1 <= version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanOrEqualSign_Null_Right()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 <= version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanOrEqualSign_Null_Left()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = new DataMinerVersion(1, 2, 3);

            // Act
            bool result = version1 <= version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_LessThanOrEqualSign_Null_Both()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 <= version2;

            // Assert
            result.Should().BeTrue();
        }
        
        #endregion

        #region GreaterThanSign

        [TestMethod]
        public void DataMinerVersion_GreaterThanSign_Success()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 4);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3));

            // Act
            bool result = version1 > version2;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanSign_Fail()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3), 5);

            // Act
            bool result = version1 > version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanSign_Null_Right()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 > version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanSign_Null_Left()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = new DataMinerVersion(1, 2, 3);

            // Act
            bool result = version1 > version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanSign_Null_Both()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 > version2;

            // Assert
            result.Should().BeFalse();
        }
        
        #endregion
        
        #region LessThanOrEqualSign

        [TestMethod]
        public void DataMinerVersion_GreaterThanOrEqualSign_Success()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(new Version(1, 2, 3), 5);
            DataMinerVersion version2 = new DataMinerVersion(1, 2, 3);

            // Act
            bool result = version1 >= version2;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanOrEqualSign_Fail()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = new DataMinerVersion(new Version(1, 2, 3), 4);

            // Act
            bool result = version1 >= version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanOrEqualSign_Null_Right()
        {
            // Arrange
            DataMinerVersion version1 = new DataMinerVersion(1, 2, 3);
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 >= version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanOrEqualSign_Null_Left()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = new DataMinerVersion(1, 2, 3);

            // Act
            bool result = version1 >= version2;

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void DataMinerVersion_GreaterThanOrEqualSign_Null_Both()
        {
            // Arrange
            DataMinerVersion version1 = null;
            DataMinerVersion version2 = null;

            // Act
            bool result = version1 >= version2;

            // Assert
            result.Should().BeTrue();
        }
        
        #endregion
    }
}