﻿using System;

namespace DependencyInjection_Service.Services
{
    public interface ISingletonService : IService { }

    public class SingletonService : ISingletonService
    {
        private string _guid;

        public SingletonService()
        {
            _guid = Guid.NewGuid().ToString();
        }
        
        public string GetGuid()
        {
            return _guid;
        }
    }
}
