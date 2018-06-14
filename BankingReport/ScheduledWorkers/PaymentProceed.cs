using BankingReport.DbContexts;
using BankingReport.Models;
using System;
using System.Linq;
using System.Threading;

namespace BankingReport.ScheduledWorkers
{
    public class PaymentProceed
    {
        /// <summary>
        /// Starts the processing.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void StartProcessing(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                ProceedPaymentRandom();
            }
            catch (Exception ex)
            {
                ProcessCancellation();
            }
        }

        /// <summary>
        /// Processes the cancellation.
        /// </summary>
        private void ProcessCancellation()
        {
            Thread.Sleep(10000);
        }

        /// <summary>
        /// Proceed the payment.
        /// </summary>
        public void ProceedPaymentRandom()
        {
            using (DbContext _db = new DbContext())
            {
                var user = _db.Users.FirstOrDefault(e => e.CreditCards.Count() != 0);
                if (user != null)
                {
                    var card = user.CreditCards.First();
                    if (card != null)
                    {
                        Random rnd = new Random();
                        ProceedPaymentOperation(card, user, _db, rnd.Next(1, 10000));
                    }
                }
            }
        }

        /// <summary>
        /// Proceed the payment fro specific user and specific card.
        /// </summary>
        public void ProceedPaymentForUser(Guid userId, Guid cardId)
        {
            using (DbContext _db = new DbContext())
            {
                var user = _db.Users.FirstOrDefault(e => e.UserId == userId);
                if (user != null)
                {
                    var card = user.CreditCards.FirstOrDefault(e => e.CardId == cardId);
                    if (card != null)
                    {
                        Random rnd = new Random();
                        ProceedPaymentOperation(card, user, _db, rnd.Next(1, 10000));
                    }
                }
            }
        }

        /// <summary>
        /// Proceeds the payment operation.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <param name="user">The user.</param>
        /// <param name="_db">The database.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        private void ProceedPaymentOperation(CreditCard card, User user, DbContext _db, int amount)
        {
            Payment payment = new Payment();
            payment.Amount = amount;
            payment.CreditCard = card;
            payment.PaymentStatus = ModelEnums.PaymentStatusEnum.Pending;
            payment.TransDate = DateTime.Now;
            payment.User = user;
            _db.Payments.Add(payment);
            _db.SaveChanges();
        }
    }
}