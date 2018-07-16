import json
import os
from flask import Flask, send_from_directory

app = Flask(__name__)
version_list = list()
descriptor_list = list()

@app.route('/svn/', methods=['GET', 'POST'])
def fetch_latest():
    ret_dict = zip(version_list, descriptor_list)
    return json.dumps(ret_dict)

@app.route("/svn/get/<version>", methods=['GET', 'POST'])
def download_update_package(version):
    return send_from_directory(os.getcwd() + "\\packages\\", "UpdateFile_" + version + ".zip", as_attachment=True)

def init_version():
    f = open("versions.yuri")
    line = f.readline()
    while line:
        lineitem = line.strip().split('\t')
        version_list.append(lineitem[0])
        descriptor_list.append(lineitem[1])
        line = f.readline()
    f.close()

if __name__ == '__main__':
    init_version()
    app.run(port=10523)
