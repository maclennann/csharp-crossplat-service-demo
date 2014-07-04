include apt
include ruby
include bundler

node default {

	# apt-get isntall these packages
	$packages = ["software-properties-common", "git", "mono-complete", "ruby-dev", "rpm"]
    
    Package { ensure=>installed }
    
	# add the monoxide ppa for recent mono versions
    apt::ppa { "ppa:directhex/monoxide":
        require	=> File["/etc/apt/sources.list.d"]
    }
    
	# exec an apt-get update so we know we have the latest package listing
    exec { "get-latest-mono":
        command 	=> "apt-get update",
        path 			=> "/usr/bin",
        require 		=> Apt::Ppa["ppa:directhex/monoxide"]
    }
    
	# install packages listed above
	# installing packages via array was apparently an "accidental feature"
	# and a coincidence of implementation. it doesn't work in newer versions
	# so we'll likely need to change it eventually
	# https://projects.puppetlabs.com/issues/22557
    package { "install_packages":
        name 		=> $packages,
        ensure 	=> present,
        require 		=> Exec["get-latest-mono"]
    }
	
	bundler::install { "/vagrant":
		user 				=> vagrant,
		group 			=> vagrant,
		deployment 	=> false
	}
		
}