# -*- mode: ruby -*-
# vi: set ft=ruby :

# Vagrantfile API/syntax version. Don't touch unless you know what you're doing!
VAGRANTFILE_API_VERSION = "2"

Vagrant.configure(VAGRANTFILE_API_VERSION) do |config|
  
   # Define two VMs, Ubuntu and Fedora
	config.vm.define "ubuntu" do |ubuntu|
		ubuntu.vm.box = "ffuenf/ubuntu-13.10-server-amd64"
		ubuntu.vm.box_url = "https://vagrantcloud.com/ffuenf/ubuntu-13.10-server-amd64"
		
		# Make sure our modules are installed
		ubuntu.vm.provision :shell do |shell|
			# For some reason, I chose a vagrant box without puppet already installed...
			shell.inline = "apt-get install -y puppet-common;
								puppet module install puppetlabs-stdlib --force;
								puppet module install puppetlabs-concat --force;
								puppet module install puppetlabs/apt --force;
								puppet module install puppetlabs-ruby --force;
								puppet module install ploperations-bundler --force;"
		end
		
		ubuntu.vm.provision :puppet do |puppet|
			puppet.manifests_path = "assets/puppet/manifests"
			puppet.manifest_file  = "site.pp"
		end
		
		ubuntu.vm.provider "virtualbox" do |v|
			v.memory = 1024
		end
		
		ubuntu.vm.network "forwarded_port", guest: 3579, host: 3579
	end
end
