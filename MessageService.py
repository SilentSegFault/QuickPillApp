import socket
import json

def handle_message(msg, conn):
    print("request", msg)
    if msg == "Msg/Get/Config":
        config = get_config()
        conn.send(bytes(json.dumps(config), 'ascii'))
    elif msg.startswith("Msg/Update/Config"):
        update_config(msg)
    elif msg == "Msg/Get/Schedule":
        schedule = get_schedule()
        conn.send(bytes(json.dumps(schedule), 'ascii'))
    elif msg.startswith("Msg/Update/Schedule"):
        update_schedule(msg)

def update_config(msg):
    with open("DeviceConfig.json", "r") as jsonFile:
        data = json.load(jsonFile)

    newConfig = json.loads(msg[17:])
    data["Config"] = newConfig

    with open("DeviceConfig.json", "w") as jsonFile:
        json.dump(data, jsonFile)

def get_config():
    f = open("DeviceConfig.json")
    data = json.load(f)
    f.close()
    return data["Config"]

def get_schedule():
    f = open("DeviceSchedule.json")
    data = json.load(f)
    f.close()
    return data["Schedule"]

def update_schedule(msg):
    with open("DeviceSchedule.json", "r") as jsonFile:
        data = json.load(jsonFile)

    newSchedule = json.loads(msg[19:])
    data["Schedule"].append(newSchedule)

    with open("DeviceSchedule.json", "w") as jsonFile:
        json.dump(data, jsonFile)

def is_message_disconnect(msg):
    if(msg == "Msg/Disconnect"):
       return True
    return False

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as sock:
    sock.bind(("0.0.0.0", 11001))
    sock.listen(2)
    running = True
    while running:
        print("Waiting for connection...")
        conn, adr = sock.accept()
        with conn:
            print(f"Connection from {adr}")
            while True:
                print("Waiting for request...")
                data = conn.recv(3 * 1024)
                if not data:
                    break
                msg = data.decode("ascii")
                if(is_message_disconnect(msg)):
                     print("Disconnecting user...")
                     break
                handle_message(msg, conn)
                
            print("User disconnected")
            #running = input("Continue? [y/n] ") == "y"
