using osu.Framework.Testing;

namespace Hitsounder.Game.Tests.Visual
{
    public abstract partial class HitsounderTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new HitsounderTestSceneTestRunner();

        private partial class HitsounderTestSceneTestRunner : HitsounderGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
