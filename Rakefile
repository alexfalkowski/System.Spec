require 'albacore'

CURRENT_PATH = File.expand_path(File.dirname(__FILE__))
VERSION = '3.1.3'
ARTIFACTS_PATH = File.join(CURRENT_PATH, 'artifacts')

def is_windows
  RbConfig::CONFIG['host_os'] =~ /mingw/
end

desc 'Get all the referenced packages'
exec :packages do |command|
  FileUtils.rm_rf('packages')
  command.command = 'tools/nuget'
  command.parameters 'install packages.config -o packages'
end

desc 'Build the solution'

if is_windows
  msbuild :build => :assembly_info do |build|
    FileUtils.rm_rf(ARTIFACTS_PATH)
    build.solution = 'System.Spec.sln'
    build.properties = { :configuration => :Release, :OutputPath => ARTIFACTS_PATH }
    build.targets :Rebuild
    build.verbosity = 'quiet'
    build.parameters '/nologo'
  end

  msbuild :build_extension => :assembly_info do |build|
    FileUtils.rm_rf(ARTIFACTS_PATH)
    build.solution = 'System.Spec.VisualStudio.Extension/System.Spec.VisualStudio.Extension.csproj'
    build.properties = { :configuration => :Release, :OutputPath => ARTIFACTS_PATH }
    build.targets :Rebuild
    build.verbosity = 'quiet'
    build.parameters '/nologo'
  end
else
  xbuild :build => :assembly_info do |build|
    FileUtils.rm_rf(ARTIFACTS_PATH)
    build.solution = 'System.Spec.sln'
    build.properties = { :configuration => :Release, :OutputPath => ARTIFACTS_PATH }
    build.targets :Rebuild
    build.verbosity = 'quiet'
    build.parameters '/nologo'
  end
end

desc 'Run the specs'
nunit :specs => :build do |nunit|
  file = (is_windows && Dir['packages/**/nunit-console.exe'].first) || 'tools/nunit-console'
  nunit.command = file
  nunit.options '-noshadow'
  nunit.assemblies 'artifacts/System.Spec.Specs.dll'
  nunit.parameters '-nologo'
end

desc 'Version the product'
task :assembly_info => [:spec_assembly_info, :spec_console_assembly_info, :spec_command_assembly_info, :spec_visualstudio_assembly_info, :spec_visualstudio_extension_assembly_info ]

desc 'Version the System.Spec assembly'
assemblyinfo :spec_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'System.Spec'
  asm.title = 'System.Spec'
  asm.description = 'System.Spec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.namespaces 'System'
  asm.custom_attributes :CLSCompliantAttribute => false
  asm.output_file = 'System.Spec/Properties/AssemblyInfo.cs'
end

desc 'Version the System.Spec console'
assemblyinfo :spec_console_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'System.Spec.Console'
  asm.title = 'System.Spec.Console'
  asm.description = 'System.Spec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.namespaces 'System'
  asm.custom_attributes :CLSCompliantAttribute => false
  asm.output_file = 'System.Spec.Console/Properties/AssemblyInfo.cs'
end

desc 'Version the System.Spec.Command assembly'
assemblyinfo :spec_command_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'System.Spec.Command'
  asm.title = 'System.Spec.Console'
  asm.description = 'System.Spec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.namespaces 'System'
  asm.custom_attributes :CLSCompliantAttribute => false
  asm.output_file = 'System.Spec.Command/AssemblyInfo.cs'
end

desc 'Version the System.Spec.VisualStudio assembly'
assemblyinfo :spec_visualstudio_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'System.Spec.VisualStudio'
  asm.title = 'System.Spec.Console'
  asm.description = 'System.Spec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.namespaces 'System'
  asm.custom_attributes :CLSCompliantAttribute => false
  asm.output_file = 'System.Spec.VisualStudio/Properties/AssemblyInfo.cs'
end

desc 'Version the System.Spec.VisualStudio.Extension assembly'
assemblyinfo :spec_visualstudio_extension_assembly_info do |asm|
  asm.version = VERSION
  asm.company_name = 'alex.falkowski'
  asm.product_name = 'System.Spec.VisualStudio.Extension'
  asm.title = 'System.Spec.Console'
  asm.description = 'System.Spec is testing tool for the C# programming language.'
  asm.copyright = Time.now.strftime('%Y')
  asm.namespaces 'System'
  asm.custom_attributes :CLSCompliantAttribute => false
  asm.output_file = 'System.Spec.VisualStudio.Extension/Properties/AssemblyInfo.cs'
end

desc 'Create the nuspec'
nuspec :nuget_spec do |nuspec|
  nuspec.id = 'System.Spec'
  nuspec.version = VERSION
  nuspec.authors = 'alex.falkowski'
  nuspec.description = 'System.Spec'
  nuspec.title = 'System.Spec'
  nuspec.language = 'en-AU'
  nuspec.projectUrl = 'https://github.com/alexfalkowski/System.Spec'
  nuspec.iconUrl = 'http://2.bp.blogspot.com/-u9nKVHQrC9E/T8g6ecVm-tI/AAAAAAAAA9Q/Sn9SDRcZyyc/s1600/RSpec_logo-07.PNG'
  nuspec.working_directory = 'artifacts'
  nuspec.output_file = 'System.Spec.nuspec'

  lib_folder = 'lib/net45'
  nuspec.file 'System.Spec.dll', lib_folder
  nuspec.file 'System.Monad.dll', lib_folder
  nuspec.file 'FluentAssertions.dll', lib_folder
  nuspec.file 'NSubstitute.dll', lib_folder
  nuspec.file 'nunit.framework.dll', lib_folder

  nuspec.file 'System.Spec.dll', 'tools'
  nuspec.file 'System.Spec.Command.dll', 'tools'
  nuspec.file 'spec.exe', 'tools'
  nuspec.file 'spec.sh', 'tools'
  nuspec.file 'System.Monad.dll', 'tools'
  nuspec.file 'FluentAssertions.dll', 'tools'
  nuspec.file 'NSubstitute.dll', 'tools'
  nuspec.file 'PowerArgs.dll', 'tools'
  nuspec.file 'nunit.framework.dll', 'tools'
end

desc 'Create the nuget package'
nugetpack :nuget_pack => :nuget_spec do |nuget|
  nuget.command = 'tools/nuget'
  nuget.nuspec = 'artifacts/System.Spec.nuspec'
  nuget.output = 'artifacts'
end

desc 'Push the nuget package'
nugetpush :nuget_push => :nuget_pack do |nuget|
  nuget.command = 'tools/nuget'
  nuget.apikey = '30ab3856-f684-4d0e-9e5e-b8282cdf6fa7'
  nuget.package = "artifacts/System.Spec.#{VERSION}.nupkg"
end
