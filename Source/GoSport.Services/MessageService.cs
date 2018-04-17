using GoSport.Data.Common.Repository;
using GoSport.Data.Models;
using GoSport.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSport.Services
{
    public class MessageService : IMessageService
    {
        private IDeletableEntityRepository<User> usersDb;
        private IDeletableEntityRepository<Message> messagesDb;

        public MessageService(IDeletableEntityRepository<User> userService, IDeletableEntityRepository<Message> messageService)
        {
            this.usersDb = userService;
            this.messagesDb = messageService;
        }
        public void SendMessage(string recieverId, string senderId, string content)
        {
            var reciever = usersDb.All().FirstOrDefault(x => x.Id == recieverId);
            var author = usersDb.All().FirstOrDefault(x => x.Id == senderId);

            if (author != null && reciever != null)
            {
                var message = new Message() { Reciever = reciever, Author = author, Content = content };
                messagesDb.Add(message);
                messagesDb.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("Invalid user ids");
            }
        }

        public IQueryable<Message> GetAllMessages(string recieverId, string senderId)
        {
            if (recieverId != null && senderId != null)
            {
                return messagesDb
                    .All()
                    .Where(x => (x.AuthorId == senderId && x.RecieverId == recieverId) || (x.AuthorId == recieverId && x.RecieverId == senderId));
            }
            else
            {
                throw new ArgumentNullException("Invalid user ids");
            }
        }
    }
}
