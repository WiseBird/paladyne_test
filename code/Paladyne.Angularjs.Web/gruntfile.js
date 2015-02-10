module.exports = function (grunt) {
	'use strict'; 
	grunt.initConfig({
		ngAnnotate: {
			options: {
			    singleQuotes: true,
			    add: true,
                remove: true
			},
			app: {
				files: [
					{
						expand: true,
						src: ['App/**/*.js'],
					}
				],
			},
		},
	});

	grunt.loadNpmTasks('grunt-ng-annotate');
};