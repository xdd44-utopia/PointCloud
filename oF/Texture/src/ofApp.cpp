#include "ofApp.h"
#include <stdlib.h>
#include <time.h>

#define NN 4096

Mat matUpdate;
Mat matOrigin;
Mat matTarget;

struct Pt {
	int x = -1;
	int y = -1;
	int dx = -1;
	int dy = -1;
	int speed = rand() % 20 + 20;
	
	void init() {
		int picker = rand() % 4;
		speed = rand() % 20 + 20;
		
		picker = rand() % 4;
		switch (picker) {
			case 0: {
				x = rand() % WW;
				y = 0;
				dx = 0;
				dy = 1;
				break;
			}
			case 1: {
				x = rand() % WW;
				y = HH - 1;
				dx = 0;
				dy = -1;
				break;
			}
			case 2: {
				x = 0;
				y = rand() % HH;
				dx = 1;
				dy = 0;
				break;
			}
			case 3: {
				x = WW - 1;
				y = rand() % HH;
				dx = -1;
				dy = 0;
				break;
			}
		}
	}
	
	void update() {
		for (int i=0;i<speed;i++) {
			x += dx;
			y += dy;
			if (x < 0 || x >= WW || y < 0 || y >= HH) {
				init();
			}
			for (int c=0;c<3;c++) {
				matUpdate.at<Vec4b>(y, x)[c] = matTarget.at<Vec3b>(y, x)[c];
			}
			matUpdate.at<Vec4b>(y, x)[3] = 255;
		}
	}
};

Pt points[NN];
int fadeRate = 8;

//--------------------------------------------------------------
void ofApp::setup(){
	
	srand(time(NULL));

	imgOrigin.load("Origin.jpg");
	matOrigin = toCv(imgOrigin);
	imgTarget.load("Target.jpg");
	matTarget = toCv(imgTarget);
	
	matUpdate = Mat::zeros(HH, WW, CV_8UC4);
	matCanvas = Mat::zeros(HH, WW, CV_8UC3);
}

//--------------------------------------------------------------
void ofApp::update(){
	for (int k=0;k<NN;k++) {
		points[k].update();
	}
	for (int i=0;i<WW;i++) {
		for (int j=0;j<HH;j++) {
			matUpdate.at<Vec4b>(j, i)[3] = ((int)matUpdate.at<Vec4b>(j, i)[3] - fadeRate > 0 ? (int)matUpdate.at<Vec4b>(j, i)[3] - fadeRate : 0);
			for (int c=0;c<3;c++) {
				matCanvas.at<Vec3b>(j, i)[c] = ((int)matUpdate.at<Vec4b>(j, i)[c] * (int)matUpdate.at<Vec4b>(j, i)[3] + (int)matOrigin.at<Vec3b>(j, i)[c] * (255 - (int)matUpdate.at<Vec4b>(j, i)[3])) / 255;
			}
		}
	}
}

void ofApp::blending(int x, int y) {
	
}

//--------------------------------------------------------------
void ofApp::draw(){
	drawMat(matCanvas, 0, 0);
}

//--------------------------------------------------------------
void ofApp::keyPressed(int key){

}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseEntered(int x, int y){

}

//--------------------------------------------------------------
void ofApp::mouseExited(int x, int y){

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){ 

}
