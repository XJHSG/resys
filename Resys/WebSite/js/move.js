function getStyle(obj,name) {                  //JS获取非行间样式
	if(obj.currentStyle) {    
		return obj.currentStyle[name];        //兼容IE
	}
	else {
		return getComputedStyle(obj, false)[name];      //兼容FF
	}
}

function startMove(obj, json, fnEnd) {         //obj:某个运动的物体 attr：要修改的属性 iTarget：要达到的效果
	clearInterval(obj.timer);                 //在调用定时器时要把上一个定时器关了
	obj.timer=setInterval(function() {       //开一个运动的定时器
		for(var attr in json) {
			var bStop=true;               //当所有都达到指定值时，才关掉定时器
			var cur=0;
			
			//取物体当前的属性值
			if(attr=='opacity') {
				cur=Math.round(parseFloat(getStyle(obj,attr))*100); //Math.round()四舍五入
			}
			else {
				cur=parseInt(getStyle(obj, attr)); //parseInt 速度取整
			}
			
			var speed=(json[attr]-cur)/6;   
			speed=speed>0?Math.ceil(speed):Math.floor(speed); //
			
			if(cur!=json[attr]) {            //当有一个属性值没达到 就不关闭定时器
				bStop=false;				
			}
			
			if(attr=='opacity') {
				obj.style.filter='alpha(opacity:'+(cur+speed)+')'; //兼容IE
				obj.style.opacity=(cur+speed)/100;
			}
			else {
				obj.style[attr]=cur+speed+'px';
			}			
		}
		
		if(bStop) {
			clearInterval(obj.timer); //obj.timer一个物体对应一个定时器
			if(fnEnd) fnEnd(); 
		}
	},30);
}