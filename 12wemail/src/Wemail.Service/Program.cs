using Wemail.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<MailDTO> mails = new List<MailDTO>() 
{
    new MailDTO(){ Id = 1 , Subject = "juster���ʼ�1" },
    new MailDTO(){ Id = 2 , Subject = "juster���ʼ�2" }
};

//��ȡ�ʼ��б�
app.MapGet("/getmails", () =>
{
    return mails;
});

//�����ʼ�idɾ���ʼ�
app.MapDelete("/delmail/{id}", (int id) => 
{
    string resultMsg = string.Empty;
    var mail = mails.FirstOrDefault(i=>i.Id == id);
    if (mail != null)
    {
        mails.Remove(mail);
        resultMsg = "delete complated.";
    }
    else
    {
        resultMsg = "nothing to do !";
    }
    return resultMsg;
});

app.Run();