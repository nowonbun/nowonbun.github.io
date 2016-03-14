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
			byte[] data = new byte[1024];
			String strBuffer = "";
			while((receiver.read(data)) > 0){
				strBuffer += new String(data);
				byte[] buffer = strBuffer.replace("\0", "").getBytes();
				if(buffer[buffer.length -4] == 13 && buffer[buffer.length -3] == 10 && 
					buffer[buffer.length -2] == 13 && buffer[buffer.length -1] == 10)
					break;
			}
			System.out.println(strBuffer);
			String filename = headerFile(strBuffer);
			if("/".equals(filename)){
				filename = "/index.html";
			}
			String filepaht = "E:\\document\\workspace_blog\\html\\web" + filename;
			byte[] contents = filedata(filepaht);
			String message = "";
			if(contents != null){
				message = "HTTP/1.1 200 OK\r\n"
						+ "Server: MyServer\r\n"
						+ "Cache-Control: no-store, no-cache, must-revalidate\r\n"
				 		+ "Content-Length: "+contents.length+"\r\n"
						+ "Keep-Alive: timeout=15, max=100\r\n"
						+ "Connection: Keep-Alive\r\n"
						+ "Content-Type: text/html\r\n\r\n";
				sender.write(message.getBytes());
				sender.write(contents);
			}else{
				message = "HTTP/1.1 204 no content\r\n"
						+ "Server: MyServer\r\n"
						+ "Cache-Control: no-store, no-cache, must-revalidate\r\n"
						+ "Keep-Alive: timeout=15, max=100\r\n"
						+ "Connection: Keep-Alive\r\n"
						+ "Content-Type: text/html\r\n\r\n";
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
	private String headerFile(String data){
		String[] buffer = data.split("\r\n");
		buffer = buffer[0].split(" ");
		return buffer[1];
	}
	//204
	private byte[] filedata(String filepath){
		File file = new File(filepath);
		if(file.exists()){
			try{
				FileInputStream in = null;
				try{
					in = new FileInputStream(file);
					int length = (int)file.length();
					byte[] data = new byte[length];
					in.read(data, 0, data.length);
					return data;
				}catch(Exception e){
					throw e;
				}finally{
					in.close();
				}
			}catch(Exception e){
				e.printStackTrace();
				return null;
			}
		}else{
			return null;
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
