#version 330 core

in vec2 aPosition;
in vec3 aColor;

uniform float scaleFactor;

out vec3 fColor;

void main() {
	gl_Position = vec4(aPosition * scaleFactor, 0, 1);
	fColor = aColor;
}
