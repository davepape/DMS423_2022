/* Script to be run with nodejs.
   Functions as a simple server that keeps track of how many times you've clicked. */

const express = require('express');
const bodyParser = require('body-parser');
var urlencodedParser=bodyParser.urlencoded({extended:false});

let app = express();
app.get('/', function (req,res) { res.send('<p>Hello World</p>'); });

let totalClicks = 0;

function receivedata(req, res) {
    if (req.body.click)
        totalClicks++;
    res.json({clicks:totalClicks});
    }

app.post('/doclick', urlencodedParser, receivedata);

let server = app.listen(8079, function () {console.log("server started");});
