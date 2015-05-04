using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSuiteExercise
{
    [TestClass]
    public class LruCacheTests
    {
        [TestMethod]
        public void MaxSizeIsSetCorrectly()
        {
            var sut = new LruCache(4);

            Assert.AreEqual(4, sut.GetMaxSize());
        }

        [TestMethod]
        public void ReturnsNullWhenItemNotFound()
        {
            var sut = new LruCache(4);
            var result = sut.Get("key");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ItemInsertedIntoCacheIsFound()
        {
            var sut = new LruCache(4);
            sut.Put("key", "value");
            var result = sut.Get("key");

            Assert.IsNotNull(result);
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void OldestItemIsRemovedWhenOverCapcity()
        {
            var sut = new LruCache(1);
            sut.Put("Eric", 1);
            sut.Put("Stan", 2);

            Assert.IsNull(sut.Get("Eric"));
            Assert.IsNotNull(sut.Get("Stan"));
        }

        [TestMethod]
        public void CacheDoesNotExceedMaximumSize()
        {
            var sut = new LruCache(2);
            sut.Put("Eric", 1);
            sut.Put("Stan", 2);
            sut.Put("Kyle", 3);

            Assert.IsNotNull(sut.Get("Kyle"));
            Assert.IsNotNull(sut.Get("Stan"));
            Assert.IsNull(sut.Get("Eric"));
        }

        [TestMethod]
        public void SettingNewValueForExistingKeyOverwritesPreviousValue()
        {
            var sut = new LruCache(2);
            sut.Put("Eric", 1);
            sut.Put("Eric", 2);

            Assert.IsNotNull(sut.Get("Eric"));
            Assert.AreEqual(2, sut.Get("Eric"));
        }

        //"saving" instead of "setting"??
        [TestMethod]
        public void SettingNewValueForExistingKeyDoesNotCauseAnyItemsToBeRemoved()
        {
            var sut = new LruCache(2);
            sut.Put("Eric", 1);
            sut.Put("Stan", 2);
            sut.Put("Stan", 5);

            Assert.IsNotNull(sut.Get("Eric"));
            Assert.IsNotNull(sut.Get("Stan"));
        }

        //"retrieve" instead of "get"???
        [TestMethod]
        public void GettingAnItemMovesItToTheTopOfTheRecentlyUsedList()
        {
            var sut = new LruCache(2);
            sut.Put("Eric", 1);
            sut.Put("Stan", 2);
            
            sut.Get("Eric");
            sut.Put("Kyle", 3);

            Assert.IsNull(sut.Get("Stan"));
            Assert.IsNotNull(sut.Get("Eric"));
            Assert.IsNotNull(sut.Get("Kyle"));
        }

        [TestMethod]
        public void SettingAnItemMovesItToTheTopOfTheRecentlyUsedList()
        {
            var sut = new LruCache(2);
            sut.Put("Eric", 1);
            sut.Put("Stan", 2);

            sut.Put("Eric", 10);
            sut.Put("Kyle", 3);

            Assert.IsNull(sut.Get("Stan"));
            Assert.IsNotNull(sut.Get("Eric"));
            Assert.IsNotNull(sut.Get("Kyle"));
        }

        [TestMethod]
        public void ToStringReturnsKeysAndValuesInOrderOfMostRecentlyUsed()
        {
            var sut = new LruCache(2);
            sut.Put("Eric", 1);
            sut.Put("Stan", 2);

            var result = sut.ToString();
            var expectedResult = string.Format("Key: Stan, Value: 2{0:s}Key: Eric, Value: 1{0:s}", Environment.NewLine);
            Assert.AreEqual(expectedResult, result);
        }

    }
}
