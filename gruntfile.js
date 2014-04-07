/*global module*/
/*jshint maxlen: false */
module.exports = function (grunt) {
  'use strict';

  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    buildDir: 'build',
    jshint: {
      options: {
        jshintrc: '.jshintrc'
      },
      gruntfile: [
        'gruntfile.js'
      ]
    },
    clean: {
      build: {
        src: '<%= buildDir %>'
      }
    },
    run_grunt: {
      options: {
        cwd: 'Gva.Web/App/',
        gruntOptions: {
          fullVersion: ''//will be set later
        }
      },
      webPackage: {
        options: {
          task: ['bundled']
          
        },
        src: ['Gva.Web/App/Gruntfile.js']
      },
      webRelease: {
        options: {
          task: ['release']
        },
        src: ['Gva.Web/App/Gruntfile.js']
      }
    },
    setAssemblyVersion: {
      options: {
        version: '',//will be set later
        fullVersion: ''//will be set later
      },
      all: {
        src: [
          '**/Properties/AssemblyInfo.cs',
          '!Common.Database/Properties/AssemblyInfo.cs',
          '!Docs.Database/Properties/AssemblyInfo.cs',
          '!Gva.Database/Properties/AssemblyInfo.cs',
          '!Regs.Database/Properties/AssemblyInfo.cs',
          '!Regs.Api.Tests/Properties/AssemblyInfo.cs',
          '!Docs.Api.Tests/Properties/AssemblyInfo.cs',
          '!Gva.MigrationTool/Properties/AssemblyInfo.cs'
        ]
      }
    },
    msbuild: {
      webDebug: {
        src: ['Gva.Web/Gva.Web.csproj'],
        options: {
          projectConfiguration: 'Debug',
          targets: ['Package'],
          stdout: true,
          verbosity: 'minimal',
          buildParameters: {
            'PlatformTarget': 'x64',
            'PackageLocation': '../<%= buildDir %>/gva.zip',//location is relative to the csproj file
            'DefaultDeployIisAppPath': 'Gva',
            'IncludeAppPool': true,
            'IgnoreDeployManagedRuntimeVersion': true
          }
        }
      },
      webRelease: {
        src: ['Gva.Web/Gva.Web.csproj'],
        options: {
          projectConfiguration: 'Release',
          targets: ['Package'],
          stdout: true,
          verbosity: 'minimal',
          buildParameters: {
            'PlatformTarget': 'x64',
            'PackageLocation': '../<%= buildDir %>/gva.zip',//location is relative to the csproj file
            'DefaultDeployIisAppPath': 'Gva',
            'IncludeAppPool': true,
            'IgnoreDeployManagedRuntimeVersion': true
          }
        }
      }
    }
  });
  
  grunt.registerTask('setVersion', function() {
    var done = this.async(),
        version = grunt.config.get('pkg').version;
    
    if (!version || !/^\d+\.\d+\.\d+\.\d+$/.test(version)) {
      grunt.log.error('package.json does not have version in the format major.minor.build.revision!');
      done(false);
      return;
    }
    
    grunt.util.spawn({
      cmd: 'git',
      args: ['rev-parse', 'HEAD']
    }, function (error, result) {
      var revision,
          fullVersion;
      if (error) {
        grunt.log.error(error);
        done(false);
      } else {
        revision = result.toString().substr(0, 6);
        fullVersion = version + '#' + revision;
        grunt.config.set('setAssemblyVersion.options.version', version);
        grunt.config.set('setAssemblyVersion.options.fullVersion', fullVersion);
        grunt.config.set('run_grunt.options.gruntOptions.fullVersion', fullVersion);
        grunt.log.ok('FullVersion is ' + fullVersion);
        done(true);
      }
    });
  });
  
  grunt.registerMultiTask('setAssemblyVersion', function() {
    var options = this.options({ version: '', fullVersion: '' }),
        version = options.version,
        fullVersion = options.fullVersion,
        origPreserveBOM = grunt.file.preserveBOM,
        versionRegEx = /\[assembly:\s*AssemblyVersion\(".*?"\)\]/g,
        fileVersionRegEx = /\[assembly:\s*AssemblyFileVersion\(".*?"\)\]/g,
        informationalVersionRegEx = /\[assembly:\s*AssemblyInformationalVersion\(".*?"\)\]/g,
        success = true;
    
    function replace(str, regex, newSubStr, errorMsg) {
      if (str.match(regex)) {
        return str.replace(regex, newSubStr);
      } else {
        grunt.log.error(errorMsg);
        success = false;
        return false;
      }
    }
    
    grunt.file.preserveBOM = true;
    this.filesSrc.forEach(function(file) {
      if (!success) {
        return;
      }
    
      if (grunt.file.exists(file)) {
        grunt.log.ok(file);
      } else {
        grunt.log.error('AssemblyInfo file \'' + file + '\' not found.');
        success = false;
        return;
      }
      
      var contents = grunt.file.read(file);

      if (contents) {
        contents = replace(
          contents,
          versionRegEx,
          '[assembly: AssemblyVersion("' + version + '")]',
          'Cannot find AssemblyVersion in file.');
      }

      if (contents) {
        contents = replace(
          contents,
          fileVersionRegEx,
          '[assembly: AssemblyFileVersion("' + version + '")]',
          'Cannot find AssemblyFileVersion in file.');
      }
      
      if (contents) {
        contents = replace(
          contents,
          informationalVersionRegEx,
          '[assembly: AssemblyInformationalVersion("' + fullVersion + '")]',
          'Cannot find AssemblyInformationalVersion in file.');
      }

      if (contents) {
        grunt.file.write(file, contents);
      }
    });
    
    grunt.file.preserveBOM = origPreserveBOM;
    return success;
  });

  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-msbuild');
  grunt.loadNpmTasks('grunt-run-grunt');

  grunt.registerTask('package', [
    'jshint:gruntfile',
    'clean',
    'setVersion',
    'setAssemblyVersion',
    'run_grunt:webPackage',
    'msbuild:webDebug'
  ]);
  
  grunt.registerTask('release', [
    'jshint:gruntfile',
    'clean',
    'setVersion',
    'setAssemblyVersion',
    'run_grunt:webRelease',
    'msbuild:webRelease'
  ]);
    
  grunt.registerTask('default', ['package']);
};
