using NLog;
public class TicketFile
{
  // public property
  public string filePath { get; set; }
  public List<HelpTicket> Tickets { get; set; }
  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "//nlog.config").GetCurrentClassLogger();


  // constructor is a special method that is invoked
  // when an instance of a class is created
  public TicketFile(string ticketFilePath)
  {
    filePath = ticketFilePath;
    
    Tickets = new List<HelpTicket>();

    // to populate the Ticket list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(filePath);
  
      while (!sr.EndOfStream)
      { 
        // create instance of HelpTicket class
        HelpTicket ticket = new HelpTicket();
        string line = sr.ReadLine();      
       
        string[] ticketDetails = line.Split('|');
        ticket.ticketId = UInt64.Parse(ticketDetails[0]);
        ticket.summary = ticketDetails[1];
        ticket.status = ticketDetails[2];
        ticket.priority = ticketDetails[3];
        ticket.submitter = ticketDetails[4];
        ticket.assigned = ticketDetails[5];
        ticket.watcher = ticketDetails[6];
        ticket.severity = UInt64.Parse(ticketDetails[7]);
              
        Tickets.Add(ticket);
      }
      // close file when done
      sr.Close();
      logger.Info("Tickets in file {Count}\n", Tickets.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }

  // method to add tickets to file
  public void AddTicket(HelpTicket ticket)
  {
    try
    {
      StreamWriter sw = new StreamWriter(filePath, true);

      sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",ticket.ticketId,ticket.summary, ticket.status,ticket.priority,ticket.submitter,ticket.assigned,ticket.watcher,ticket.severity);
      sw.Close();
      // add ticket details to Lists
      Tickets.Add(ticket);
      // log transaction
      logger.Info("Ticket id {ticketId} added\n", ticket.ticketId);
    } 
    catch(Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
}