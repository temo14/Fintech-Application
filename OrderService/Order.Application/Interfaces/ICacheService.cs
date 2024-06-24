﻿namespace Order.Application.Interfaces;

public interface ICacheService
{
    T GetCachedData<T>(string key);
    void SetCachedData<T>(string key, T data, TimeSpan cacheDuration);
    void DeleteCachedData(string key);
}