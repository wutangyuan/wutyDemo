// See https://aka.ms/new-console-template for more information

using Quartz;
using Quartz.Impl;

Console.WriteLine("Hello, World!");

IJobDetail job = JobBuilder.Create<TestJob>()
    .WithIdentity("TestJob", "myGroup")
    .Build();


//ITrigger trigger = TriggerBuilder.Create().WithIdentity("TestJobTrigger", "myGroup")
//    .WithSimpleSchedule(x =>
//    {
//        x.WithIntervalInSeconds(1).RepeatForever();
//    })
//    .Build();


ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("TestJobTrigger", "myGroup")
    .WithCronSchedule("0 15 10 * * ?")
    .Build();


StdSchedulerFactory factory = new StdSchedulerFactory();
//创建任务调度器
IScheduler scheduler = await factory.GetScheduler();
//启动任务调度器
await scheduler.Start();

//将创建的任务和触发器条件添加到创建的任务调度器当中
await scheduler.ScheduleJob(job, trigger);

Console.WriteLine("任务调度器已启动，按任意键退出...");
Console.ReadKey();



/// <summary>
/// 创建一个测试的Job类
/// </summary>
public class TestJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"{DateTime.Now.ToString("yy-MM-dd HH:mm:ss fff")},执行了TestJob");
        await Task.CompletedTask;
    }
}