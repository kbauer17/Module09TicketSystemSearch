public abstract class Ticket
{
  public UInt64 ticketId { get; set; }
  public string summary { get; set; }
  public string status{get;set;}
  public string priority{get;set;}
  public string submitter{get;set;}
  public string assigned{get;set;}
  public string watcher{get;set;}
  

  // method to display tickets
  public virtual string Display()
    {
      return $"Id: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatcher: {watcher}\n"; 
    }
}