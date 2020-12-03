using System;

namespace Advent.Runner
{
    internal class PipelineBuilder : IPipelineBuilder
    {
        internal IPipeline Build()
        {
            return new Pipeline();
        }
    }
}