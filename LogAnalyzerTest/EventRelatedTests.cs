using System;
using LogAnalyzer;
using NSubstitute;
using NUnit.Framework;

namespace LogAnalyzerTest
{
    [TestFixture]
    public class EventRelatedTests
    {
        [Test]
        public void ctor_WhenViewIsLoaded_CallsViewRender()
        {
            var mockView = Substitute.For<IView>();
            var p = new Presenter(mockView);

            /*Чтобы сгенерировать событие в тесте, на него надо подписать-
            ся.Конструкция выглядит нелепо, но она необходима, чтобы
            компилятор не ругался, поскольку свойства, относящиеся к
            событиям, обрабатываются специальным образом и бдительно
            охраняются компилятором.Напрямую генерировать событие
            может только класс или структура, в которой событие объяв -
            лено.*/

            mockView.Loaded += Raise.Event<Action>();
            mockView.Received()
                .Render(Arg.Is<string>(s => s.Contains("Hello World")));
        }

    }
}