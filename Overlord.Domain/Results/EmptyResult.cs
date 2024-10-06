﻿namespace Overlord.Domain.Results
{
    public sealed class EmptyResult
    {
        private EmptyResult()
        {
        }

        public static readonly EmptyResult Default = new();
    }
}
