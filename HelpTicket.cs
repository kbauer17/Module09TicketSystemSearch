public class HelpTicket : Ticket
{
  public UInt64 severity { get; set; }
  

  // method to display tickets
  public override string Display()
    {
      return $"Id: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatcher: {watcher}\nSeverity: {severity}\n"; 
    }
}