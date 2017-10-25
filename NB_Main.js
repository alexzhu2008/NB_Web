function ScollToEnd() {
    alert("now I will scoll text of record to end");
    var txtRecord = document.getElementById("txt_Record");
    document.body.focus(); // 加上这个
    txtRecord.scrollTop = txtRecord.scrollHeight;        // IE，FF，这句就是滚动 

    }
		

