using NLog;
public class EnhancementFile
{
  // public property
  public string filePath { get; set; }
  public List<Enhancement> Enhancements { get; set; }
  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "//nlog.config").GetCurrentClassLogger();



  public EnhancementFile(string enhancementFilePath)
  {
    filePath = enhancementFilePath;
    
    Enhancements = new List<Enhancement>();

    // to populate the Enhancement list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(filePath);
  
      while (!sr.EndOfStream)
      { 
        // create instance of Enhancement class
        Enhancement item = new Enhancement();
        string line = sr.ReadLine();      
       
        string[] enhancementDetails = line.Split('|');
        item.ticketId = UInt64.Parse(enhancementDetails[0]);
        item.summary = enhancementDetails[1];
        item.status = enhancementDetails[2];
        item.priority = enhancementDetails[3];
        item.submitter = enhancementDetails[4];
        item.assigned = enhancementDetails[5];
        item.watcher = enhancementDetails[6];
        item.software = enhancementDetails[7];
        item.cost = enhancementDetails[8];
        item.reason = enhancementDetails[9];
        item.estimate = UInt64.Parse(enhancementDetails[10]);
              
        Enhancements.Add(item);
      }
      // close file when done
      sr.Close();
      logger.Info("Items in file {Count}\n", Enhancements.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }

  // method to add tickets to file
  public void AddEnhancement(Enhancement enhancement)
  {
    try
    {
      StreamWriter sw = new StreamWriter(filePath, true);

      sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}",enhancement.ticketId,enhancement.summary, enhancement.status,enhancement.priority,enhancement.submitter,enhancement.assigned,enhancement.watcher,enhancement.software,enhancement.cost,enhancement.reason,enhancement.estimate);
      sw.Close();
      // add item details to Lists
      Enhancements.Add(enhancement);
      // log transaction
      logger.Info("Enhancement id {ticketId} added\n", enhancement.ticketId);
    } 
    catch(Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
}