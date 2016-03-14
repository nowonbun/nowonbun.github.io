import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;


public class Client extends Socket implements Runnable{

	private Server parent = null;
	private OutputStream sender = null;
	private InputStream receiver = null;
	public Client(Server server){
		parent = server;
	}
	@Override
	public void run() {
		try{
			int size = 0;
			byte[] data = new byte[1024];
			String strBuffer = "";
			while(size = receiver.read(data)) > 0){
				strBuffer += new String(data);
				byte[] buffer = strBuffer.replace("\0","").getBytes();
				if(buffer[buffer.length -4] == 13 && buffer[buffer.length -3] == 10 &&
					buffer[buffer.length -2] == 13 && buffer[buffer.length -1] == 10){
					break;
				}
				System.out.println(strBuffer);
				String message = "HTTP/1.1 200 OK\r\n"
						+ "Server: MyServer\r\n"
						+ "Cache-Control: no-store, no-cache, must-revalidate\r\n"
				 		+ "Content-Length: 10\r\n"
						+ "Keep-Alive: timeout=15, max=100\r\n"
						+ "Connection: Keep-Alive\r\n"
						+ "Content-Type: text/html\r\n\r\n";
				message = "helloworld";
				sender.write(message.getBytes());

			}
		}catch(Exception e){
			e.printStackTrace();
		}finally{
			try{
				this.close();
			}catch(Exception e){
				e.printStackTrace();
			}
			parent.deleteClient(this);
		}
	}

	public void start(){
		try{
			initialize();
			(new Thread(this)).start();
		}catch(Exception e){
			e.printStackTrace();
		}
	}
	private void initialize() throws Exception{
		sender = getOutputStream();
		receiver = getInputStream();
	}
}
