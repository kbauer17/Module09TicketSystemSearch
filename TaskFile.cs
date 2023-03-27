using NLog;
public class TaskFile
{
  // public property
  public string filePath { get; set; }
  public List<Task> Tasks { get; set; }
  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "//nlog.config").GetCurrentClassLogger();



  public TaskFile(string taskFilePath)
  {
    filePath = taskFilePath;
    
    Tasks = new List<Task>();

    // to populate the Task list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(filePath);
  
      while (!sr.EndOfStream)
      { 
        // create instance of Task class
        Task item = new Task();
        string line = sr.ReadLine();      
       
        string[] taskDetails = line.Split('|');
        item.ticketId = UInt64.Parse(taskDetails[0]);
        item.summary = taskDetails[1];
        item.status = taskDetails[2];
        item.priority = taskDetails[3];
        item.submitter = taskDetails[4];
        item.assigned = taskDetails[5];
        item.watcher = taskDetails[6];
        item.projectName = taskDetails[7];
        item.dueDate = taskDetails[8];
              
        Tasks.Add(item);
      }
      // close file when done
      sr.Close();
      logger.Info("Items in file {Count}\n", Tasks.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }

  // method to add items to file
  public void AddTask(Task task)
  {
    try
    {
      StreamWriter sw = new StreamWriter(filePath, true);

      sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",task.ticketId,task.summary, task.status,task.priority,task.submitter,task.assigned,task.watcher,task.projectName,task.dueDate);
      sw.Close();
      // add item details to Lists
      Tasks.Add(task);
      // log transaction
      logger.Info("Enhancement id {ticketId} added\n", task.ticketId);
    } 
    catch(Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
}