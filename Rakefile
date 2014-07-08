require 'albacore'
require 'open-uri'
require 'fileutils'
require 'os'

BUILD_CONFIG = 'Debug'
TOOLS = File.expand_path("tools")
NUGET = File.expand_path("#{TOOLS}/NuGet")
WIX = File.expand_path("#{TOOLS}/wix")

desc 'Do it all'
task :make => [:retrieve,:build,:bundle]

desc 'Build the solution (msbuild or xbuild)'
task :build do
	# Use msbuild in .net and xbuild in mono
	if OS.windows?
		Rake::Task["net_build"].execute
	else
		Rake::Task["mon_build"].execute
	end
end

desc 'Bundle and installer (msi or deb)'
task :bundle do
# Use msbuild in .net and xbuild in mono
	if OS.windows?
		Rake::Task["wix_bootstrap"].execute
		Rake::Task["msi_bundle"].execute
	else
		Rake::Task["lin_bundle"].execute
	end
end

desc 'Retrieve nuget dependencies'
task :retrieve do
	Rake::Task["nuget_fetch"].execute
end

msbuild :net_build do |b|
	b.verbosity = 'normal'
	b.solution = "TopShelfTest.sln"
	b.properties = { :Configuration => BUILD_CONFIG }
end

xbuild :mon_build do |b|
	b.verbosity = 'normal'
	b.solution = "TopShelfTest.sln"
	b.properties = { :Configuration => BUILD_CONFIG }
end

task :msi_bundle => :wix_bootstrap do
	sh "\"#{WIX}/heat.exe\" @installers/heat.rsp"
	sh "\"#{WIX}/candle.exe\" @installers/candle.rsp"
	sh "\"#{WIX}/light.exe\" @installers/light.rsp"
end

task :lin_bundle do
	if !FileTest.exist?("target/installers/TopShelfTest")
		FileUtils.mkdir_p("target/installers/TopShelfTest")
	end
	
	sh "fpm -s dir -t deb -n TopShelfTest -v 0.0.3 -p target/installers/TopShelfTest/topshelftest-0.0.3.deb --prefix /opt/TopShelfTest --after-install TopShelfTest/scripts/postinstall.sh --before-remove TopShelfTest/scripts/preuninstall.sh /vagrant/TopShelfTest/bin/Debug"
	sh "fpm -s dir -t rpm -n TopShelfTest -v 0.0.3 -p target/installers/TopShelfTest/topshelftest-0.0.3.rpm --prefix /opt/TopShelfTest --after-install TopShelfTest/scripts/postinstall.sh --before-remove TopShelfTest/scripts/preuninstall.sh /vagrant/TopShelfTest/bin/Debug"
end

# If we don't have a copy of nuget, download it
task :nuget_bootstrap do
	puts 'Ensuring NuGet exists in tools/NuGet'
	
	if !FileTest.exist?("#{NUGET}/nuget.exe")
		puts 'Downloading nuget from nuget.org'
		
		FileUtils.mkdir_p("#{NUGET}")
		File.open("#{NUGET}/nuget.exe", "wb") do |file|
			file.write open('http://nuget.org/nuget.exe').read
		end
	end
end

task :wix_bootstrap do
	puts 'Ensuring WiX exists in tools/wix'
	
	if !FileTest.exist?(WIX)
		if !FileTest.exist?("#{TOOLS}/wix.zip")
			puts 'Downloading wix from codeplex.com'
			
			File.open("#{TOOLS}/wix.zip", "wb") do |file|
				file.write open('http://download-codeplex.sec.s-msft.com/Download/Release?ProjectName=wix&DownloadId=762938&FileTime=130301249355530000&Build=20919').read
			end
		end

		puts 'Extracting WiX archive to #{WIX}'
		Zip::ZipFile.open("#{TOOLS}/wix.zip") do |zip|
			zip.each do |f|
				path = File.join(WIX, f.name)
				FileUtils.mkdir_p(File.dirname(path))
				zip.extract(f,path) unless File.exist?(path)
			end
		end
		
	end
end


# Fetch nuget dependencies for all packages
task :nuget_fetch => :nuget_bootstrap do
	
	# If we aren't running under windows, assume we're using mono
	CMD_PREFIX = ""
	if !OS.windows?
		CMD_PREFIX = "mono"
	end	

	FileList["**/packages.config"].each { |filepath|
		sh "#{CMD_PREFIX} \"#{NUGET}/nuget.exe\" i \"#{filepath}\" -o packages"
	}
end	