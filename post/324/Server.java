import java.io.IOException;
import java.net.ServerSocket;
import java.util.ArrayList;

public class Server extends ServerSocket implements Runnable{
	private ArrayList<Client> clients = null;
	public Server(int port) throws IOException {
		super(port);
		clients = new ArrayList<Client>();
	}
	public Client accept() throws IOException{
		if (isClosed())
		    throw new IOException("Socket is closed");
		if (!isBound())
		    throw new IOException("Socket is not bound yet");
		Client s = new Client(this);
		implAccept(s);
		s.start();
		return s;
	}
	@Override
	public void run() {
		while(true){
			try{
				clients.add(this.accept());
				System.out.println("Connection");
			}catch(IOException e){
				e.printStackTrace();
			}
		}
	}
	public void start(){
		(new Thread(this)).start();
	}
	public void deleteClient(Client client){
		clients.remove(client);
	}
}
