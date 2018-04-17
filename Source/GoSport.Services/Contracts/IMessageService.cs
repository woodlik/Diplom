using GoSport.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Services.Contracts
{
    public interface IMessageService
    {
        void SendMessage(string recieverId, string senderId, string content);

        IQueryable<Message> GetAllMessages(string recieverId, string senderId);
    }
}
