[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener
{
    public override void Connected(BoltConnection connection)
    {
        var log = PlayerJoinedEvent.Create();
        log.Message = string.Format("{0} Player connected", connection.RemoteEndPoint);
        log.Send();
    }

    public override void Disconnected(BoltConnection connection)
    {
        var log = PlayerJoinedEvent.Create();
        log.Message = string.Format("{0} Player disconnected", connection.RemoteEndPoint);
        log.Send();
    }
}