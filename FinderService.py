import socket

name = "TestDevice"

def is_hello_valid(hello):
    if hello == "Msg/Hello/1.0":
        return True
    return False

with socket.socket(socket.AF_INET, socket.SOCK_DGRAM, socket.IPPROTO_UDP) as sock:
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    sock.bind(("0.0.0.0", 11000))
    print("listening on port 11000...")
    while True:
        data, addr = sock.recvfrom(1024)
        msg = data.decode("ascii")
        if is_hello_valid(msg):
            sock.sendto(bytes(name, "ascii"), addr)
            print("Connection request from:", addr)