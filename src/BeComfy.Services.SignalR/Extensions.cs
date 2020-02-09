using System;

namespace BeComfy.Services.SignalR
{
    public static class Extensions
    {
        public static string ToUserGroup(this Guid userId) 
            => $"users:{userId}";

        public static string ToUserGroup(this string userId)
            => $"users:{userId}";
    }
}