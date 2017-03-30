namespace ECERP.Core.Tests
{
    using System;
    using System.Globalization;
    using Xunit;

    public class CommonHelperTests
    {
        [Fact]
        public void Can_get_typed_value()
        {
            // string conversion
            var i = 1000;
            var stringConvertedValue = CommonHelper.To<string>(i);
            Assert.Equal(typeof(string), stringConvertedValue.GetType());
            Assert.Equal("1000", stringConvertedValue);

            // int conversion
            var intConvertedValue = CommonHelper.To<int>("1000");
            Assert.Equal(typeof(int), intConvertedValue.GetType());
            Assert.Equal(1000, intConvertedValue);

            // datetime conversion
            var date = DateTime.UtcNow.Date;
            var dateStr = date.ToString(CultureInfo.InvariantCulture);
            var dateConvertedValue = CommonHelper.To<DateTime>(dateStr);
            Assert.Equal(typeof(DateTime), dateConvertedValue.GetType());
            Assert.Equal(date, dateConvertedValue);
        }
    }
}
