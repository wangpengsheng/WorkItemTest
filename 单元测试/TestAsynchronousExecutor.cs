using AsynchronousExecutor;

namespace 单元测试
{
    [TestClass]
    public class TestAsynchronousExecutor
    {
        [TestMethod]
        public async Task TestMethod1()
        {
<<<<<<< HEAD
=======
            var executor = new AsyncExecutor<int>(ExecutionMode.Parallel);
            executor.AddTask(async () => await Task.FromResult(1));
            executor.AddTask(async () => await Task.FromResult(2));
            var result = await executor.StartAsync();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
>>>>>>> 887dfb4f68b3383f670c71f69d7d49fc17226f5d
        }
    }
}