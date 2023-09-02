using Employee.Common.PushNotification;
using Employee.Data.IRepository;

namespace Employee.Api;

public  class ScheduleNotification
{
    private readonly IEmployeeInformationRepository informationRepository;
    private readonly IEmailService emailService;



    public ScheduleNotification(IEmployeeInformationRepository informationRepository, IEmailService emailService)
    {
        this.informationRepository = informationRepository;
        this.emailService = emailService;
    }

    public async void pushNotification()
    {
        var userData = await informationRepository.GetEmployeeList();
        foreach(var item in userData)
        {
            MailRequest req = new MailRequest();
            req.ToMail = item.Email;
            req.Subject = "Hurry UP Offer End Today !!!!!!!!!";
            req.Body = "  Hi "+item.Name+"  A limited-time offer is any kind of discount, deal, special gift, or reward a buyer can get if they make a purchase from you during a certain time period";
            await emailService.SendMailAsync(req);
        }
    }
}
