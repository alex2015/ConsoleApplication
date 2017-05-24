using System;

namespace LogAnalyzer
{
    public interface IView
    {
        event Action Loaded;
        void Render(string text);
    }
}
